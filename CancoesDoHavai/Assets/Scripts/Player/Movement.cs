using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    InputAction jump;
    InputAction move;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int maxJumps = 2;

    Rigidbody2D rb;

    private void Awake()
    {

    }
    void Start()
    {

    }

    private void OnMove()
    {
        rb.linearVelocity = new Vector2(move.ReadValue<Vector2>().x * speed, rb.linearVelocity.y);
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
