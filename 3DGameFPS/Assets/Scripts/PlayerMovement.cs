using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    private Rigidbody playerRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame  
    void FixedUpdate() // Corrected method name from 'fixdUpdate' to 'FixedUpdate'  
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        playerRigidbody.linearVelocity = move * moveSpeed; // Corrected 'linearVelocity' to 'velocity'  
    }

    public void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>(); // Corrected 'Velocity2' to 'Vector2'  
    }
}
