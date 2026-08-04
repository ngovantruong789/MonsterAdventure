using System;
using System.Collections;
using UnityEngine;

public class SceneReceiverData : LifetimeScope
{
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    private SceneLoadModel _sceneLoadModel;
    public SceneLoadModel SceneLoadModel => _sceneLoadModel;
    public Action<SceneLoadModel> SceneLoadedEvent { get; set; }

    public void Initialize(SceneLoadModel data)
    {
        StartCoroutine(ReceiverSceneLoadModelCoroutine(data));
    }

    private IEnumerator ReceiverSceneLoadModelCoroutine(SceneLoadModel data)
    {
        _sceneLoadModel = new SceneLoadModel();
        _sceneLoadModel = data;
        yield return new WaitForSeconds(0.5f);

        _sceneLoadManager.EndLoadNewScene();
        if (_sceneLoadModel != null)
        {
            SceneLoadedEvent?.Invoke(_sceneLoadModel);
        }
    }
}
