using System;
using UnityEngine;

[Serializable]
public class PlayerMovementInstaller : BaseInstaller
{
    [SerializeField] private PlayerMovementView _view;
    [SerializeField] private PlayerMovement _playerMovement;

    private PlayerMovementPresenter _presenter;

    public override void Initialize()
    {
        _presenter = new PlayerMovementPresenter(_playerMovement, _view);
        Debug.Log("PlayerMovementInstaller initialized");
    }
}
