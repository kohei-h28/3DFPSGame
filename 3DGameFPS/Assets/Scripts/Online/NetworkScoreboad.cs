using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using System;

/// <summary>
/// RequireComponent�͂��̃N���X���g���Ƃ��ɕK�{�̃R���|�\�l���g���w�肷��
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
    /// ScoreEntry(Id�����X�R�A���Ǘ����Ă���f�[�^)�̃��X�g
    /// </summary>
    public NetworkList<ScoreEntry> Scores;

    /// <summary>
    /// Score��̎c�^�[�Q�b�g
    /// </summary>
    public NetworkVariable<int> RemalningTragets = 
        new (
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    /// <summary>
    /// Game�̃X�e�[�^�X
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
    /// �l�b�g���[�N��ɐ������ꂽ���̏���
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
            //�l�b�g���[�N�}�l�[�W���[�N�ɐڑ������Ƃ���
            //�ؒf�������̏�����ǉ�����
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;

        }
    }

    /// <summary>
    /// �l�b�g���[�N���؂ꂽ���̏���
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
    /// �X�R�A���Z��Rpc
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
            //struct�̓R�s�[���ύX���đ��
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
        //Mathf.Max(�ŏ��l,�ϐ�)�ōő�l��Ԃ��܂��B�ϐ����ŏ��l����������ꍇ
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
