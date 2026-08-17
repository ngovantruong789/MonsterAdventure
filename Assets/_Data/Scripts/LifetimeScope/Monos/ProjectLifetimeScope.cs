using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope, IStartable
{
    [SerializeField] private List<MonsterSO> _monsters;
    [SerializeField] private ItemDatabaseSO _itemDatabaseSO;
    [SerializeField] private List<ItemSO> _items;

    void IStartable.Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        //Controller
        builder.RegisterEntryPoint<PlayerTeamController>(Lifetime.Singleton).AsSelf().As<IPlayerTeamProvider>();
        builder.RegisterEntryPoint<InventoryController>(Lifetime.Singleton).As<IInventoryProvider>();
        builder.RegisterEntryPoint<SceneLoadController>(Lifetime.Singleton).As<ISceneLoadController>();

        //Model
        builder.Register<PlayerTeamModel>(Lifetime.Singleton);
        builder.Register<InventoryModel>(Lifetime.Singleton);
        builder.Register<BattleModel>(Lifetime.Singleton);

        //Presenter
        builder.RegisterEntryPoint<SceneLoadPresenter>(Lifetime.Singleton);

        //File
        builder.RegisterInstance(_itemDatabaseSO);
        builder.RegisterInstance<IReadOnlyList<MonsterSO>>(_monsters);
        builder.RegisterInstance<IReadOnlyList<ItemSO>>(_items);

        //View
        builder.RegisterComponentInHierarchy<SceneLoadView>();

        //Instance
        builder.RegisterComponentInHierarchy<SceneLoadManager>();
        builder.RegisterComponentInHierarchy<BattleManager>();
    }
}