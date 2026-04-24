using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHolder : MonoBehaviour
{
    public static PlayerInputHolder instance;
    public PlayerInput playerInput;
    void Awake()
    {
        instance = this;
        playerInput = GetComponent<PlayerInput>();
    }
}
