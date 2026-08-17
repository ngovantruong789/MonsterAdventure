using UnityEngine;
using VContainer;

public class PlayerEntity : CharacterEntity, IPlayer
{
    [Inject] private SceneLoadManager _sceneLoadManager;
    [Inject] private IPlayerTeamProvider _playerTeamProvider;
    [Inject] private BattleModel _battleModel;
    [Inject] private IPlayerMovement _playerMovement;
    public IPlayerMovement PlayerMovement => _playerMovement;

    public bool CanBattle => _playerTeamProvider.CanBattle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MonsterEntity monsterEntity)) return;
        if (!_playerTeamProvider.CanBattle) return;

        _battleModel.OpponentMonsterModel = monsterEntity.CurrentMonsterModel;
        _sceneLoadManager.StartLoadScene("BattleScene");
    }
}