using UnityEngine;

public class PlayerMovementPresenter
{
    private PlayerMovementView _view;
    private PlayerMovement _playerMovement;

    public PlayerMovementPresenter(PlayerMovement playerMovement, PlayerMovementView playerMovementView)
    {
        _view = playerMovementView;
        _playerMovement = playerMovement;

        _view.MoveEvent += OnJoystickPosChanged;
        Debug.Log("PlayerMovementPresenter initialized");
    }

    private void OnJoystickPosChanged(Vector2 direction, float speedIntensity)
    {
        _playerMovement.ChangePos(direction, speedIntensity);
    }
}
