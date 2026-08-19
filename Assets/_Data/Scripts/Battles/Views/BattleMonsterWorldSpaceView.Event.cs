using System;
using UniRx;

public partial class BattleMonsterWorldSpaceView
{
    private Subject<AnimationCompletedViewData> _onAnimationCompletedViewData = new();
    public IObservable<AnimationCompletedViewData> OnAnimationCompletedViewData => _onAnimationCompletedViewData;

    private Subject<EItemType> _onActiveItemCompleted = new();
    public IObservable<EItemType> OnActiveItemCompleted => _onActiveItemCompleted;

    private Subject<EMonsterSide> _onVFXCompleted = new();
    public IObservable<EMonsterSide> OnVFXCompleted => _onVFXCompleted;
}