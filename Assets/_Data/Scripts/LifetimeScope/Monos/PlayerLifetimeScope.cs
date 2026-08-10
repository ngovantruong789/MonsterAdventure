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
        //Mono
        builder.RegisterComponentInHierarchy<SceneLoadManager>();
        builder.RegisterComponentInHierarchy<PlayerEntity>();
    }
}
