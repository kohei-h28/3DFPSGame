using UnityEngine;
using Unity.Netcode;
using System.Collections;
using Unity.VisualScripting;

public class ScoreBoardSpawner : MonoBehaviour
{
    [SerializeField]
    private NetworkObject scoreboardPrefab;

    private bool spawned = false;
    /// <summary>
    /// GameObjectがScene上にactiveになった時に走る処理
    /// </summary>
    private void OnEnable()
    {
        TryHookOrSpown();
    }

    private void OnDisable()
    {
        if(NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
        }
    }

    private void TryHookOrSpown()
    {
        var nw = NetworkManager.Singleton;
        if(nw == null)
        {
            StartCoroutine(WaitForNetworkManager());
            return;
        }
        // すでにサーバーが動いている場合は即スポーンする
    if (nw.IsListening && nw.IsServer )
    {
        SpawnIfServer();
    }
    else
        {
            nw.OnServerStarted += OnServerStarted;
        }

    }
    

    private void OnServerStarted()
    {
        SpawnIfServer();
        NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
    }


    private void SpawnIfServer()
    {
        if (spawned)
        {
            return;
        }
        var nm = NetworkManager.Singleton;
        if(nm == null || !nm.IsServer)
        {
            return;

        }

        if(NetworkScoreboad.Instance != null &&
            NetworkScoreboad.Instance.NetworkObject.IsSpawned)
        {
            spawned = true;
            return;
        }

        var scoreboard = Instantiate(scoreboardPrefab);
        scoreboard.Spawn(true);
        spawned = true;
        Debug.Log("Scoreboard Spawned");


    }

    IEnumerator WaitForNetworkManager()
    {
        while(NetworkManager.Singleton == null)
        {
            yield return null;
        }
        TryHookOrSpown();
    }
}
