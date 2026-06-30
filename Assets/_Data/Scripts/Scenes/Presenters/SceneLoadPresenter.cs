using System;

public class SceneLoadPresenter
{
    private SceneLoadModel _sceneLoadModel;
    private SceneLoadView _sceneLoadView;
    private SceneLoadManager _sceneLoadManager;

    public SceneLoadPresenter(SceneLoadView sceneLoadView, SceneLoadManager sceneLoadManager)
    {
        _sceneLoadView = sceneLoadView;
        _sceneLoadManager = sceneLoadManager;
    }

    public void ToggleLoadScene(Action onComplete = null)
    {
        _sceneLoadView.ToggleOpenCloseLoadScene(onComplete);
    }

    public void SetSceneLoadModel(SceneLoadModel sceneLoadModel)
    {
        _sceneLoadModel = sceneLoadModel;
    }
}
