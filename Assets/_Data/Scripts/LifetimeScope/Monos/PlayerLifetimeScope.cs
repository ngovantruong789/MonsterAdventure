using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope, IStartable
{
    void IStartable.Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected override void Configure(IContainerBuilder builder)
    {
        //Class
        builder.RegisterEntryPoint<PlayerMovementPresenter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerMonsterTeamPresenter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerInventoryPresenter>(Lifetime.Singleton);

        //Mono
        builder.RegisterComponentInHierarchy<PlayerEntity>().As<IPlayer>();
        builder.RegisterComponentInHierarchy<PlayerAnimatorController>();
        builder.RegisterComponentInHierarchy<PlayerMovementView>();
        builder.RegisterComponentInHierarchy<PlayerMovement>().As<IPlayerMovement>();
        builder.RegisterComponentInHierarchy<HUDMonsterTeamView>();
        builder.RegisterComponentInHierarchy<HUDInventoryView>();

        //Firebase
        builder.RegisterEntryPoint<FirebaseInitializer>(Lifetime.Singleton).As<IFirebaseInitializer>();
        builder.RegisterEntryPoint<PlayerDataController>(Lifetime.Singleton).As<IPlayerData>();
    }
}