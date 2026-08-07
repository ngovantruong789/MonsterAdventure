using System;
using UniRx;

public partial class AnimationStateBehaviour
{
    private static Subject<StateExitedData> _onStateExited = new();
    public static IObservable<StateExitedData> OnStateExited => _onStateExited;
}