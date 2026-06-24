using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : LifetimeScope, IStartInit
{
    private Player_Input inputActions;

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
        inputActions = new Player_Input();
        inputActions.Player.Move.performed += HandleMovePerformed;
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
        LogKey(ctx.control.name);
    }

    private void LogKey(string keyName)
    {
        Debug.Log($"Phím vừa ấn: {keyName}");
    }
}