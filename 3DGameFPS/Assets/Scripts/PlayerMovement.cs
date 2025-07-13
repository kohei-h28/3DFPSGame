﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;//RotationConstraint用

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    public float moveSpeed = 5f;

    private Rigidbody playerRigidbody;

    /// <summary>
    /// プレイヤーが向いている方向の基準
    /// 視点操作を別オブジェクトに任せている場合はそのTransdformを指定
    /// </summary>
    [SerializeField]
    private Transform lookTransform;

    [SerializeField]
    private RotationConstraint rotationConstraint;//RotationConstraint参照

    private bool isAiming = false;//Aim状態フラグ

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // カーソルのモードを変更し、カーソル自体も非表示にします
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // 視点の前方向のベクトルと右方向のベクトルを取得
        Vector3 forward = lookTransform.forward;
        Vector3 right = lookTransform.right;

        //上下成分を除去し、地面にそった移動ベクトルにする
        forward.y = 0f;
        right.y = 0f;

        // 正規化して方向のベクトルとして扱う(速度を統一)
        forward.Normalize();
        right.Normalize();

        // 前後、左右の移動入力に方向ベクトルを掛けて移動量を決定
        Vector3 move = forward * moveInput.y + right * moveInput.x;
        playerRigidbody.linearVelocity = move * moveSpeed;

        //Aim中ならRotationOffset.yを60に、そうでなければ0に
        Vector3 offset = rotationConstraint.rotationOffset;
        offset.y = isAiming ? 60f : 0f;
        rotationConstraint.rotationOffset = offset;
    }
    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    //Aim入力用の関数
    public void OnAiming(InputValue value)
    {
        isAiming = value.isPressed;
    }
}
