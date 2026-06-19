using UnityEngine;

public class PlayerMovementPresenter
{
    private PlayerMovementView _view;
    private PlayerMovement _playerMovement;
    private PlayerAnimatorController _animatorController;

    public PlayerMovementPresenter(PlayerMovement playerMovement, PlayerMovementView playerMovementView, PlayerAnimatorController animatorController)
    {
        _view = playerMovementView;
        _playerMovement = playerMovement;
        _animatorController = animatorController;

        _view.MoveEvent += OnJoystickPosChanged;
        Debug.Log("PlayerMovementPresenter initialized");
    }

    private void OnJoystickPosChanged(Vector2 direction, float speedIntensity)
    {
        _playerMovement.ChangePos(direction, speedIntensity);
        if(speedIntensity == 0)
        {
            _animatorController.SetMovementState(0);
        }
        else
        {
            _animatorController.SetMovementState(1);
        }
    }
}
