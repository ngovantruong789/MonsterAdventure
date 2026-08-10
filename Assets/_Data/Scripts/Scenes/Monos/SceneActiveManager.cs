using UnityEngine;
using System.Collections.Generic;

public class SceneActiveManager : BaseMonoBehaviour
{
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private Camera _camera;

    [SerializeField] private string _currentScene;
    public string CurrentScene => _currentScene;

    [SerializeField] private List<GameObject> disableObjects = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        _sceneLoadManager = FindAnyObjectByType<SceneLoadManager>();
        _sceneLoadManager.UpdateCurrentScene(this);
    }

    public void SetActiveObjects(bool isActive)
    {
        foreach (GameObject obj in disableObjects)
        {
            obj.SetActive(isActive);
        }
    }

    public Camera GetCurrentCameraObject()
    {
        return _camera;
    }
}
