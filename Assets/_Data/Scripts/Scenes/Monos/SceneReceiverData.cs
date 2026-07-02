using System;
using UnityEngine;

public class SceneReceiverData : LifetimeScope
{
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    private SceneLoadModel _sceneLoadModel;
    public Action<SceneLoadModel> SceneLoadedEvent;

    protected override void Start()
    {
        base.Start();
        Debug.Log("SceneReceiverData ended");
        _sceneLoadManager.EndLoadNewScene();
        if(_sceneLoadModel != null)
        {
            SceneLoadedEvent.Invoke(_sceneLoadModel);
        }
    }

    public void Initialize(SceneLoadModel data)
    {
        _sceneLoadModel = new SceneLoadModel();
        _sceneLoadModel = data;
        //Debug.Log(_sceneLoadModel.BatlleModel.OpponentMonsterModel.Level + "; " + _sceneLoadModel.BatlleModel.OpponentMonsterModel.Health);
    }
}
