using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoadManager : LifetimeScope, IStartInit
{
    [SerializeField] private SceneLoadView _sceneLoadView;
    [SerializeField] private List<GameObject> disableObjects = new List<GameObject>();

    [SerializeField] private MonsterEntity _monsterEntity;
    private SceneLoadModel _sceneLoadModel;
    private SceneLoadInstaller _installer;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _installer = new SceneLoadInstaller(_sceneLoadModel, _sceneLoadView, this);
        _installer.Initialize();
        Debug.Log("SceneLoadInstaller Initialized");
    }

    public void StartLoadScene(string sceneName, SceneLoadModel sceneLoadModel)
    {
        StartCoroutine(StartLoadSceneCoroutine(sceneName, sceneLoadModel));
    }

    private IEnumerator StartLoadSceneCoroutine(string sceneName, SceneLoadModel sceneLoadModel)
    {
        yield return new WaitForSeconds(3);
        _installer.ToggleLoadScene(() =>
        {
            StartCoroutine(LoadSceneAdditive(sceneName, sceneLoadModel));
        });
    }

    private IEnumerator LoadSceneAdditive(string sceneName, SceneLoadModel sceneLoadModel)
    {
        yield return new WaitForSeconds(1);

        SetActiveObjects(false);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return op;

        Scene sceneLoaded = SceneManager.GetSceneByName(sceneName);
        foreach(GameObject obj in sceneLoaded.GetRootGameObjects())
        {
            if (!obj.TryGetComponent(out SceneReceiverData sceneReceiverData)) continue;

            sceneReceiverData.Initialize(sceneLoadModel);
            break;
        }
    }

    public void EndLoadNewScene()
    {
        SetActiveObjects(true);
        _installer.ToggleLoadScene();
    }

    private void SetActiveObjects(bool isActive)
    {
        foreach (GameObject obj in disableObjects)
        {
            obj.SetActive(isActive);
        }
    }
}
