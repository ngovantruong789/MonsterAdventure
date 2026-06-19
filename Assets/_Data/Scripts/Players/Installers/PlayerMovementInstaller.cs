using System;
using UnityEngine;

[Serializable]
public class PlayerMovementInstaller : BaseInstaller
{
    [SerializeField] private PlayerMovementView _view;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorController _animatorController;

    private PlayerMovementPresenter _presenter;

    public override void Initialize()
    {
        _presenter = new PlayerMovementPresenter(_playerMovement, _view, _animatorController);
        Debug.Log("PlayerMovementInstaller initialized");
    }
}
