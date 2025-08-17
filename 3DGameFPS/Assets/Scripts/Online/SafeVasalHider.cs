using UnityEngine;
using Unity.Netcode;
public class SafeVisualHider : NetworkBehaviour
{
    //
    public override void OnNetworkDespawn()
    {
        foreach(var renderer in GetComponentsInChildren<Renderer>(true))
        {
            renderer.enabled = false;
        }

        foreach(var col in GetComponentsInChildren<Collider>(true))
        {
            col.enabled = false;
        }
        gameObject.SetActive(false);
    }



}
