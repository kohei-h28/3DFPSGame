using UnityEngine;

public class bullet : MonoBehaviour
{
    // Õ“Ë‚É’e‚ªÁ‚¦‚éˆ—
    void OnCollisionEnter(Collision collision)
    {
        // ‰½‚©‚ÉÕ“Ë‚µ‚½‚ç’e‚ğÁ‚·
        Destroy(gameObject);
    }
}
