using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction makeMoneyAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        makeMoneyAction = playerInput.actions["MakeMoney"];
    }

    private void OnEnable()
    {
        makeMoneyAction.performed += ProcessMakeMoney;
    }

    private void OnDisable()
    {
        makeMoneyAction.performed -= ProcessMakeMoney;
    }

    void ProcessMakeMoney(InputAction.CallbackContext _)
    {
        Debug.Log("Money made");
    }
}
