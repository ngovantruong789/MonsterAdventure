using System;
using UniRx;
using VContainer.Unity;

public class SceneLoadPresenter : IStartable, IDisposable
{
    private readonly SceneLoadView _sceneLoadView;
    private readonly ISceneLoadController _sceneLoadController;
    private readonly CompositeDisposable _compositeDisposable = new();

    public SceneLoadPresenter(ISceneLoadController sceneLoadController, SceneLoadView sceneLoadView)
    {
        _sceneLoadView = sceneLoadView;
        _sceneLoadController = sceneLoadController;
    }

    public void Start()
    {
        _sceneLoadController.OnLoadScene
            .Subscribe(val => ToggleLoadScene(val))
            .AddTo(_compositeDisposable);

        _sceneLoadView.OnToggleCompleted
            .Subscribe(val => HandleToggleCompleted(val))
            .AddTo(_compositeDisposable);
    }

    public void ToggleLoadScene(bool isOpen)
    {
        _sceneLoadView.ToggleOpenCloseLoadScene(isOpen);
    }

    private void HandleToggleCompleted(bool isOpen)
    {
        _sceneLoadController.ToggleLoadSceneCompleted(isOpen);
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}
