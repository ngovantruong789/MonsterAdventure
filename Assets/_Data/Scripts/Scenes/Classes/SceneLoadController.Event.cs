using System;
using UniRx;

public partial class SceneLoadController
{
    private Subject<bool> _onToggleSceneCompleted = new();
    public IObservable<bool> OnToggleSceneCompleted => _onToggleSceneCompleted;

    private Subject<bool> _onLoadScene = new();
    public IObservable<bool> OnLoadScene => _onLoadScene;
}