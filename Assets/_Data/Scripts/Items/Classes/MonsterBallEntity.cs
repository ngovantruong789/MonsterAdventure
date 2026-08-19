using System;
using UniRx;

public partial class BallEntity
{
    private Subject<Unit> _onActiveCompleted = new();
    public IObservable<Unit> OnActiveCompleted => _onActiveCompleted;
}