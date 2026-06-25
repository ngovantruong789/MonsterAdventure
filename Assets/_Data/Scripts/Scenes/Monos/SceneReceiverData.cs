using UnityEngine;

public class SceneReceiverData : LifetimeScope
{
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    private SceneLoadModel _sceneLoadModel;

    protected override void Start()
    {
        base.Start();
        Debug.Log("SceneReceiverData ended");
        _sceneLoadManager.EndLoadNewScene();
    }

    public void Initialize(SceneLoadModel data)
    {
        _sceneLoadModel = new SceneLoadModel();
        _sceneLoadModel = data;
    }
}
