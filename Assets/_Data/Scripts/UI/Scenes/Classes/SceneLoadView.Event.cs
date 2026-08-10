using System;
using UniRx;

public partial class SceneLoadView
{
    private Subject<bool> _onToggleCompleted = new();
    public IObservable<bool> OnToggleCompleted => _onToggleCompleted;
}
