using UnityEngine;

public interface IServerDamageble 
{
    /// <summary>
    /// サーバーでのみ、HP減算を行う
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="attackClientId"></param>
    /// <param name="hitPoint"></param>
    void ApplyDamageServer(int damage, ulong attackClientId, Vector3 hitPoint);
}
