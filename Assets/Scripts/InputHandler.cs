using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction makeMoneyAction;

    public static event Action OnMakeMoney;

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
        OnMakeMoney?.Invoke();
    }
}
