using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHolder : MonoBehaviour
{
    public static PlayerInputHolder instance;
    public InputActionAsset playerInput;
    void Awake()
    {
        instance = this;
        playerInput.actionMaps[0].Enable();
    }
}
