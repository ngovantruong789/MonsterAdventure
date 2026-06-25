using System;
using UnityEngine;

public class SceneLoadInstaller : BaseInstaller
{
    private SceneLoadModel _sceneLoadModel;
    private SceneLoadView _sceneLoadView;
    private SceneLoadPresenter _sceneLoadPresenter;
    private SceneLoadManager _sceneLoadManager;

    public SceneLoadInstaller(SceneLoadModel sceneLoadModel, SceneLoadView sceneLoadView, SceneLoadManager sceneLoadManager)
    {
        _sceneLoadManager = sceneLoadManager;
        _sceneLoadModel = sceneLoadModel;
        _sceneLoadView = sceneLoadView;
    }

    public override void Initialize()
    {
        _sceneLoadPresenter = new SceneLoadPresenter(_sceneLoadView, _sceneLoadManager);
    }

    public void ToggleLoadScene(Action onComplete = null)
    {
        _sceneLoadPresenter.ToggleLoadScene(onComplete);
    }
}
