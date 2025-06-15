using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] // �������Ԉ���Ă�
    private Transform pitchTarget;

    [SerializeField] // ��������
    private float mouseSensitivity = 100f; // ���X�y���C��

    private float pitch = 0f;

    public void OnLook(InputValue value)
    {
        // �v���C���[�̎��_����(x�F���Ay�F�c)
        Vector2 look = value.Get<Vector2>(); // ���X�y���ƕ��@�C��

        // �������̉�]�i���E�ɃJ�������񂷁j
        float yaw = look.x * mouseSensitivity * Time.deltaTime;

        // �c�����̉�]�i�㉺�ɃJ������������j
        float pitchDelta = -look.y * mouseSensitivity * Time.deltaTime;

        // �㉺�̉�]���s�������Ȃ��悤�ɐ���
        pitch = Mathf.Clamp(pitch + pitchDelta, -80f, 80f);

        // pitch�i�㉺�j���J�����I�u�W�F�N�g�ɔ��f
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // yaw�i���E�j��{�̂ɔ��f
        this.transform.Rotate(Vector3.up * yaw);
    }
}

