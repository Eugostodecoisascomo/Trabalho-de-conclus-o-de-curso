using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    InputAction jump;
    InputAction move;

    InputActionAsset inputActions;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5000f;
    [SerializeField] int maxJumps = 3;
    int jumpsLeft;
    Vector2 moveDir;

    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        inputActions = PlayerInputHolder.instance.playerInput;
        jump = inputActions.FindAction("Jump");
        move = inputActions.FindAction("Move");

        jump.performed += Jump;
        move.performed += OnMove;
        move.canceled += OnMove;
    }
    void Start()
    {
        jumpsLeft = maxJumps;
    }

    private void Update()
    {
        if(Physics2D.Raycast(transform.position, -Vector2.up,1f,groundLayer) && jumpsLeft!=maxJumps)
        {
            jumpsLeft = maxJumps;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDir.x, rb.linearVelocityY);
}

private void OnMove(InputAction.CallbackContext value)
    {
       moveDir = value.ReadValue<Vector2>() * speed;
    }
    void Jump(InputAction.CallbackContext value)
    {
        if(jumpsLeft > 0)
        {
            rb.AddForce(Vector2.up * jumpForce*jumpsLeft, ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }
}
