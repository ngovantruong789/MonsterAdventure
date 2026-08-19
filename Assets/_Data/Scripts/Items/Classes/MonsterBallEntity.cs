using System;
using UniRx;

public partial class BallEntity
{
    private Subject<EBallState> _onActivePhaseCompleted = new();
    public IObservable<EBallState> OnActivePhaseCompleted => _onActivePhaseCompleted;
}