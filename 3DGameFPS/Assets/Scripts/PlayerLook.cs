using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] // ←ここ間違ってた
    private Transform pitchTarget;

    [SerializeField] // ←ここも
    private float mouseSensitivity = 100f; // ←スペル修正

    private float pitch = 0f;

    public void OnLook(InputValue value)
    {
        // プレイヤーの視点入力(x：横、y：縦)
        Vector2 look = value.Get<Vector2>(); // ←スペルと文法修正

        // 横方向の回転（左右にカメラを回す）
        float yaw = look.x * mouseSensitivity * Time.deltaTime;

        // 縦方向の回転（上下にカメラを向ける）
        float pitchDelta = -look.y * mouseSensitivity * Time.deltaTime;

        // 上下の回転が行きすぎないように制限
        pitch = Mathf.Clamp(pitch + pitchDelta, -80f, 80f);

        // pitch（上下）をカメラオブジェクトに反映
        pitchTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // yaw（左右）を本体に反映
        this.transform.Rotate(Vector3.up * yaw);
    }
}

