using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerEntity : CharacterEntity
{
    [Inject] private SceneLoadManager _sceneLoadManager;
    [Inject] private IPlayerTeamProvider _playerTeamProvider;
    [Inject] private BattleModel _battleModel;

    protected override void Configure(IContainerBuilder builder)
    {
        //Class
        builder.RegisterEntryPoint<PlayerMovementPresenter>(Lifetime.Singleton);

        //Mono
        builder.RegisterComponentInHierarchy<PlayerAnimatorController>();
        builder.RegisterComponentInHierarchy<PlayerMovementView>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MonsterEntity monsterEntity)) return;
        if (!_playerTeamProvider.CanBattle) return;

        _battleModel.OpponentMonsterModel = monsterEntity.CurrentMonsterModel;
        _sceneLoadManager.StartLoadScene("BattleScene");
    }
}
