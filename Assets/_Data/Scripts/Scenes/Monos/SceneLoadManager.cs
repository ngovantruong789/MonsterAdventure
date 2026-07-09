using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        _installer.ToggleLoadScene(() =>
        {
            StartCoroutine(LoadSceneAdditiveCoroutine(sceneName, sceneLoadModel));
        });
    }

    private IEnumerator LoadSceneAdditiveCoroutine(string sceneName, SceneLoadModel sceneLoadModel)
    {
        yield return new WaitForSeconds(0.5f);

        SetActiveObjects(false);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return op;

        Scene sceneLoaded = SceneManager.GetSceneByName(sceneName);
        foreach(GameObject obj in sceneLoaded.GetRootGameObjects())
        {
            if (obj.TryGetComponent(out SceneReceiverData sceneReceiverData))
            {
                sceneReceiverData.Initialize(sceneLoadModel);
            }
            if (obj.TryGetComponent(out SceneLoadManager sceneLoadManager))
            {
                sceneLoadManager.SetActiveObjects(true);
            }
        }
    }

    public void CloseSceneAttitive(string sceneNameClose, string sceneNameBack, SceneLoadModel sceneLoadModel)
    {
        _installer.ToggleLoadScene(() =>
        {
            OnCloseSceneAttitive(sceneNameClose, sceneNameBack, sceneLoadModel);
        });
    }

    private void OnCloseSceneAttitive(string sceneNameClose, string sceneNameBack, SceneLoadModel sceneLoadModel)
    {
        Scene sceneLoaded = SceneManager.GetSceneByName(sceneNameBack);
        foreach (GameObject obj in sceneLoaded.GetRootGameObjects())
        {
            if (obj.TryGetComponent(out SceneReceiverData sceneReceiverData))
            {
                sceneReceiverData.Initialize(sceneLoadModel);
            }
            if (obj.TryGetComponent(out SceneLoadManager sceneLoadManager))
            {
                sceneLoadManager.SetActiveObjects(true);
            }
        }

        SceneManager.UnloadSceneAsync(sceneNameClose);
    }

    public void EndLoadNewScene()
    {
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
