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
    [SerializeField] float jumpForce = 5000f;
    [SerializeField] float idealJumpCooldown = 0.3f;
    [SerializeField] int maxJumps = 3;
    int jumpsLeft;
    float timeSinceJump;
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
        if(Physics2D.Raycast(transform.position, -Vector2.up,1f,groundLayer) && jumpsLeft!=maxJumps)
        {
            jumpsLeft = maxJumps;
        }
        else
        {
            timeSinceJump += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDir.x, rb.linearVelocityY);
    }


    /*float GetFinalJumpForce()
    {
        
        float finalJumpForce;
        float precision = Mathf.Clamp(1 - 2*Mathf.Abs(timeSinceJump - idealJumpCooldown), 0.33f, 1);

        finalJumpForce = jumpForce*precision;

        return finalJumpForce * jumpsLeft;
    }*/
    private void OnMove(InputAction.CallbackContext value)
    {
       moveDir = value.ReadValue<Vector2>() * speed;
    }
    void Jump(InputAction.CallbackContext value)
    {
        if(jumpsLeft > 0)
        {
            rb.AddForce(Vector2.up * jumpForce * jumpsLeft, ForceMode2D.Impulse);
            //print(GetFinalJumpForce() + " " + timeSinceJump);
            jumpsLeft--;
            timeSinceJump = 0;
        }
    }
}
