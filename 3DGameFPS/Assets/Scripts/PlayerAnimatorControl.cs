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
    /// �G�C�����Ă��邩�ۂ��̃t���O
    /// </summary>
    public bool IsAiming = false;

    public void ReloadingAnimation()
    {
        playerAnimator.SetTrigger("Reload");
    }

    private void FixedUpdate()
    {
        playerAnimator.SetBool("Aiming",IsAiming);

        // �x�N�g���̑傫�����X�s�[�h�Ƃ��ďo�͂���
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
