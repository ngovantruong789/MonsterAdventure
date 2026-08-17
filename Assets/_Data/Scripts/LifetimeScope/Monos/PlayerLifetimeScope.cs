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

        //Mono
        builder.RegisterComponentInHierarchy<PlayerAnimatorController>();
        builder.RegisterComponentInHierarchy<PlayerMovementView>();
        builder.RegisterComponentInHierarchy<PlayerMovement>().As<IPlayerMovement>();
        builder.RegisterComponentInHierarchy<PlayerEntity>();
    }
}