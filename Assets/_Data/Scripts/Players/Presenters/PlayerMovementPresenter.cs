using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerMovementPresenter : IStartable
{
    private readonly PlayerMovementView _view;
    private readonly PlayerMovement _playerMovement;
    private readonly PlayerAnimatorController _animatorController;
    [Inject] private IPlayerTeamProvider _playerTeamProvider;

    public PlayerMovementPresenter(PlayerMovement playerMovement, PlayerMovementView playerMovementView, PlayerAnimatorController animatorController)
    {
        _view = playerMovementView;
        _playerMovement = playerMovement;
        _animatorController = animatorController;
        Debug.Log("PlayerMovementPresenter initialized");
    }

    public void Start()
    {
        _view.MoveEvent += OnJoystickPosChanged;
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
