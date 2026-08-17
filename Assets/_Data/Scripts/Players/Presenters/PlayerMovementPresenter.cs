using System;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public class PlayerMovementPresenter : IStartable, IDisposable
{
    private readonly PlayerMovementView _view;
    private readonly PlayerAnimatorController _animatorController;
    private readonly IPlayerMovement _playerMovement;
    private readonly IPlayerTeamProvider _playerTeamProvider;
    private readonly CompositeDisposable _disposables = new();
    private bool _isMoveable = true;

    public PlayerMovementPresenter(IPlayerTeamProvider playerTeamProvider, 
                                    IPlayerMovement playerMovement, 
                                    PlayerMovementView playerMovementView, 
                                    PlayerAnimatorController animatorController)
    {
        _view = playerMovementView;
        _playerMovement = playerMovement;
        _playerTeamProvider = playerTeamProvider;
        _animatorController = animatorController;

        _playerMovement.IsMoveable
            .Subscribe(val =>
            {
                _isMoveable = val;
                HandleToggleMove();
            })
            .AddTo(_disposables);

        Debug.Log("PlayerMovementPresenter initialized");
    }

    public void Start()
    {
        _view.MoveEvent += OnJoystickPosChanged;
    }

    public void HandleToggleMove()
    {
        if (!_isMoveable)
        {
            _animatorController.SetMovementState(0);
        }
    }

    private void OnJoystickPosChanged(Vector2 direction, float speedIntensity)
    {
        if (!_isMoveable) return;

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

    public void Dispose()
    {
        _disposables.Dispose();
    }
}