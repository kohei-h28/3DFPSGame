using System;
using UnityEngine;

public class PlayerAnimatorControl : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Rigidbody playerRigidbody;

    private float speed =0f;


    /// <summary>
    /// エイムしているか否かのフラグ
    /// </summary>
    public bool IsAiming = false;

    public void ReloadingAnimation()
    {
        playerAnimator.SetTrigger("Reload");
    }

    private void FixedUpdate()
    {
        playerAnimator.SetBool("Aiming",IsAiming);

        // ベクトルの大きさをスピードとして出力する
        speed = new Vector3(
            playerRigidbody.linearVelocity.x
            ,0
            ,playerRigidbody.linearVelocity.z).magnitude;

        playerAnimator.SetFloat("Speed",speed);
    }

    internal void SetReload()
    {
        throw new NotImplementedException();
    }
}
