using UnityEngine;
using Unity.Netcode;





[RequireComponent(typeof(NetworkObject))]
[RequireComponent (typeof(Health))]

public class NetworkScoreTarget : NetworkBehaviour,IServerDamageble
{
    [SerializeField]
    private int score = 10;

    private Health health;

    private bool handled = false;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    /// <summary>
    /// HitPorter‚ÌServerRpc
    /// </summary>
    /// <param name="dameg"></param>
    /// <param name="attckClientID"></param>
    /// <param name="hitPoint"></param>
    public void ApplyDamageServer(int damege, ulong attckClientID,Vector3 hitPoint)
    {
        if (!IsServer || handled)
        {
            return;
        }

        health.TakeDamage(damege);
        if (!health.IsDead())
        {
            return;
        }
        handled = true;

        NetworkScoreboad.Instance.AddScoreServerRpc(attckClientID, score);

        NetworkScoreboad.Instance.TargetDestroyedSeverOnly();

        var no = GetComponent<NetworkScoreboad>();
        if (no != null && no.IsSpawned)
        {
            no.OnDeferringDespawn((bool)no.IsSceneObject ? false : true);
        }
    }



}
