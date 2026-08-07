using System;
using UniRx;

public partial class SceneReceiverData
{
    private readonly Subject<SceneLoadModel> _onSceneLoaded = new();
    public IObservable<SceneLoadModel> OnSceneLoaded => _onSceneLoaded;
}