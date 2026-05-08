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
    [SerializeField] float[] jumpForce = {7, 13, 10, 7};
    [SerializeField] float jumpAngle = 80;
    [SerializeField] float jumpDelay = 0.2f;
    [SerializeField] int maxJumps = 4;
    [SerializeField] bool isGrounded = false;
    float delayTimer;
    int jumpsLeft;
    
    Vector2 moveDir;

    [SerializeField] Rigidbody2D rb;


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
        delayTimer += Time.deltaTime;
        if(delayTimer > jumpDelay)
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
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
            if(moveDir.x * rb.linearVelocityX < 0)
                rb.linearVelocity = new Vector2(Mathf.Clamp(-rb.linearVelocityX, -speed, speed), rb.linearVelocityY);
            if(moveDir.x != 0 && Mathf.Abs(rb.linearVelocityX) < 0.1f)
                rb.linearVelocity = new Vector2(speed * moveDir.x, rb.linearVelocityY);

            float jumpMultipliyer = 1;
            if(rb.linearVelocityY < -1)
                jumpMultipliyer = 0.7f;
            
            int jumpIndex = 4-jumpsLeft;
            if(!isGrounded && jumpIndex == 0)
                jumpIndex = 1;

            rb.AddForce(Quaternion.Euler(0, 0, -moveDir.x * jumpAngle) * Vector2.up * jumpForce[jumpIndex] * jumpMultipliyer, ForceMode2D.Impulse);
            jumpsLeft--;
            delayTimer = 0;
            isGrounded = false;
        }
    }
}
