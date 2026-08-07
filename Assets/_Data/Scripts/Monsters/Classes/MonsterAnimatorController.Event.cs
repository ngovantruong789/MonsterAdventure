using System;
using UniRx;

public partial class MonsterAnimatorController
{
    private Subject<MonsterAnimationCompletedData> _onAnimationCompleted = new();
    public IObservable<MonsterAnimationCompletedData> OnAnimationCompleted => _onAnimationCompleted;
}