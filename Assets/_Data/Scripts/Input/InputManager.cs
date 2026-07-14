using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : LifetimeScope, IStartInit
{
    private InputSystem_Actions inputActions;
    public Action<Vector2> MovePressedEvent { get; set; }

    protected override void Start()
    {
        Initialize();
    }

    protected override void OnDisable()
    {
        ToggleStatusInput(false);
    }

    public void Initialize()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += HandleMovePerformed;
        inputActions.Player.Move.canceled += HandleMovePerformed;

        ToggleStatusInput(true);
    }

    private void ToggleStatusInput(bool isEnable)
    {
        if (isEnable)
        {
            inputActions?.Enable();
        }
        else
        {
            inputActions?.Disable();
        }
    }

    private void HandleMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 moveValue = ctx.ReadValue<Vector2>();
        MovePressedEvent?.Invoke(moveValue);
    }
}