using UnityEngine;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] Rigidbody2D rb;

    InputAction interact;
    InputActionAsset inputActions;
    void Start()
    {
        inputActions = PlayerInputHolder.instance.playerInput;

        interact = inputActions.FindAction("Interact");
        interact.Enable();
        interact.performed += Interact;
    }
    bool GetInteractableAround(out GameObject result)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, interactionRange, Vector2.zero, 0, interactionLayer);
        if(hit.collider != null)
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }
    void Interact(InputAction.CallbackContext value)
    {
        print("Checking");
        GameObject result;
        if(GetInteractableAround(out result))
            print("Found interact");
    }
}