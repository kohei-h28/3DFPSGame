using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    private Rigidbody playerRigidbody;

    [SerializeField]
    private Transform lookTransform; // �J�����̌������g��

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        // カーソルのモードを変更し、カーソル自体も非表示にします
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // �J�����̑O�����ƉE�������擾
        Vector3 forward = lookTransform.forward;
        Vector3 right = lookTransform.right;

        // �㉺�������Ȃ����i�n�ʂ����̕����ɂ���j
        forward.y = 0f;
        right.y = 0f;

        // �����𐳂�����������
        forward.Normalize();
        right.Normalize();

        // �J�����̌����ɍ��킹�Ĉړ��x�N�g�����v�Z
        Vector3 move = forward * moveInput.y + right * moveInput.x;

        // �v���C���[�𓮂����i�� velocity �ɏC���j
        playerRigidbody.linearVelocity = move * moveSpeed;
    }
}