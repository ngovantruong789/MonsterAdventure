using VContainer;
using VContainer.Unity;

public class BattleLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        //Mono
        builder.RegisterComponentInHierarchy<BattleMonsterWorldSpaceView>();
        builder.RegisterComponentInHierarchy<HUDBattleMonsterView>();
        builder.RegisterComponentInHierarchy<UIMapBattleView>();

        //Class
        builder.Register<DamageCalculator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BattleTurnController>(Lifetime.Singleton);
        builder.Register<BattleMonsterController>(Lifetime.Singleton).As<IBattleMonsterPresenter>().As<IBattleMonsterTurn>();

        //Presenter
        builder.RegisterEntryPoint<BattleMonsterPresenter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BattleLoadingHUDPresenter>(Lifetime.Singleton);
    }
}