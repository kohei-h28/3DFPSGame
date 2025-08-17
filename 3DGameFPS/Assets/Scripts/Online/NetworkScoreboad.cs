using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using System;

/// <summary>
/// RequireComponentはこのクラスを使うときに必須のコンポ―ネントを指定する
/// </summary>
[RequireComponent(typeof(NetworkObject))]
public class NetworkScoreboad : NetworkBehaviour
{
    public static NetworkScoreboad Instance
    {

        get; 
        private set;
    }
    /// <summary>
    /// ScoreEntry(Idｔｐスコアを管理しているデータ)のリスト
    /// </summary>
    public NetworkList<ScoreEntry> Scores;

    /// <summary>
    /// Score上の残ターゲット
    /// </summary>
    public NetworkVariable<int> RemalningTragets = 
        new (
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    /// <summary>
    /// Gameのステータス
    /// </summary>
    public NetworkVariable<GameState> State =
        new (
            GameState.Playing,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);
    internal readonly bool IsSceneObject;

    private void Awake()
    {
        Instance = this;
        if(Scores == null)
        {
            Scores = new NetworkList<ScoreEntry>(
                readPerm: NetworkVariableReadPermission.Everyone,
                writePerm: NetworkVariableWritePermission.Server);
        }
    }
    /// <summary>
    /// ネットワーク上に生成された時の処理
    /// </summary>
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Scores.Clear();
            foreach(var client in NetworkManager.ConnectedClientsList)
            {
                AddPlayerIfMissing(client.ClientId);
            }
            //ネットワークマネージャー君に接続したときと
            //切断した時の処理を追加する
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;

        }
    }

    /// <summary>
    /// ネットワークが切れた時の処理
    /// </summary>
    public override void OnNetworkDespawn()
    {
        if (IsServer && NetworkManager != null)
        {
            NetworkManager.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnected;
        }
        if(Instance == this)
        {
            Instance = null;

        }
    }


    private void OnClientConnected(ulong clientId)
    {
        if (IsServer)
        {
            return;
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (!IsServer)
        {
            return;
        }
        if (IndexOf(clientId) > 0)
        {
            Scores.RemoveAt(IndexOf(clientId));
        }
    }


    private void AddPlayerIfMissing(ulong id)
    {
        if(IndexOf(id) > 0)
        {
            return;
        }
        Scores.Add(new ScoreEntry(id, 0));
    }

    private int IndexOf(ulong id)
    {
        for (int i = 0; i< Scores.Count; i++)
        {
            if (Scores[i].ClientId == id)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// スコア加算のRpc
    /// </summary>
    /// <param name="scoreClientId"></param>
    /// <param name="amount"></param>
    [ServerRpc(RequireOwnership = false)]
    public void AddScoreServerRpc(ulong scoreClientId,int amount = 1)
    {
        if(!IsServer || State.Value == GameState.GameOver)
        {
            return;
        }
        int index = IndexOf(scoreClientId);

        if(index < 0)
        {
            Scores.Add(new ScoreEntry(scoreClientId, amount));
        }

        else
        {
            //structはコピー→変更→再代入
            var score = Scores[index];
            score.Score += amount;
            Scores[index] = score;
        }
    }

    public void TargetDestroyedSeverOnly()
    {
        if(!IsServer || State.Value == GameState.GameOver)
        {
            return;
        }
        //Mathf.Max(最小値,変数)で最大値を返します。変数が最小値を下回った場合
        RemalningTragets.Value = Mathf.Max(0, RemalningTragets.Value - 1);
        if(RemalningTragets.Value == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        if (!IsServer)
        {
            return;
        }
       State.Value = GameState.GameOver;
        ShowResultClientRpc();
    }
    [ClientRpc]
    private void ShowResultClientRpc()
    {
        var leaderBoard = FindAnyObjectByType<LeaderBoradUI>();
        if (leaderBoard != null)
        {
            leaderBoard.ShowResult();
        }
    }

    internal void OnDeferringDespawn(bool v)
    {
        throw new NotImplementedException();
    }
}
