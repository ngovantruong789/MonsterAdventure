using System;
using UniRx;
using UnityEngine;

public partial class BattleMonsterWorldSpaceView
{
    private Subject<AnimationCompletedViewData> _onAnimationCompletedViewData = new();
    public IObservable<AnimationCompletedViewData> OnAnimationCompletedViewData => _onAnimationCompletedViewData;

    private Subject<EMonsterSide> _onVFXCompleted = new();
    public IObservable<EMonsterSide> OnVFXCompleted => _onVFXCompleted;
}