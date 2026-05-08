using NUnit.Framework;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class Movement : MonoBehaviour
{
    InputAction jump;
    InputAction move;

    InputActionAsset inputActions;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed = 5f;
    [SerializeField] float acceleration = 50f;
    [SerializeField] float jumpForce = 5000f;
    [SerializeField] float jumpAngle = 80;
    [SerializeField] int maxJumps = 3;
    [SerializeField] bool isGrounded = false;
    int jumpsLeft;
    Vector2 moveDir;

    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
    }
    void Start()
    {
        inputActions = PlayerInputHolder.instance.playerInput;
        jump = inputActions.FindAction("Jump");
        move = inputActions.FindAction("Move");

        jump.performed += Jump;
        move.performed += OnMove;
        move.canceled += OnMove;
        jumpsLeft = maxJumps;
    }

    private void Update()
    {   
        isGrounded = Physics2D.Raycast(transform.position, -Vector2.up,1.2f,groundLayer);
        if(isGrounded && jumpsLeft!=maxJumps)
        {
            jumpsLeft = maxJumps;
        }
    }

    void FixedUpdate()
    {
        if(isGrounded)
        {
            if(moveDir.x == 0)
            {
                rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocityX, 0, acceleration*3*Time.fixedDeltaTime), rb.linearVelocityY);
            }
            rb.AddForce(new Vector2(moveDir.x * acceleration,0));
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX, -speed, speed), rb.linearVelocityY);
        }
    }

    private void OnMove(InputAction.CallbackContext value)
    {
        moveDir = value.ReadValue<Vector2>();
    }
    void Jump(InputAction.CallbackContext value)
    {
        if(jumpsLeft > 0)
        {
            if(moveDir.x * rb.linearVelocityX < 0)
                rb.linearVelocity = new Vector2(Mathf.Clamp(-rb.linearVelocityX, -speed, speed),rb.linearVelocityY);
            rb.AddForce(Quaternion.Euler(0, 0, -moveDir.x * jumpAngle) * Vector2.up * jumpForce * jumpsLeft, ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }
}
