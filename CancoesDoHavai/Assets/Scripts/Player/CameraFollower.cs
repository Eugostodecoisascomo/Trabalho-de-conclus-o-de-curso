using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollower : MonoBehaviour
{
    InputAction jump;
    InputAction move;
    InputActionAsset inputActions;
    public GameObject aim;
    private Vector2 dir;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float offset = 2;
    
    float offsetMultipliyer = 1;

    void Start()
    {
        inputActions = PlayerInputHolder.instance.playerInput;
        jump = inputActions.FindAction("Jump");
        move = inputActions.FindAction("Move");

        move.performed += OnMove;
        move.canceled += OnMove;
    }
    void FixedUpdate()
    {
        dir = aim.transform.position + Vector3.up * offset * offsetMultipliyer  - transform.position;
        transform.position += new Vector3(dir.x, dir.y, 0) * Time.deltaTime * speed;
    }

    void OnMove(InputAction.CallbackContext value)
    {
        float verticalInput = value.ReadValue<Vector2>().y; 
        if(verticalInput > 0)
            offsetMultipliyer = 3;
        else if(verticalInput < 0)
            offsetMultipliyer = -1;
        else
            offsetMultipliyer = 1;
    }
}
