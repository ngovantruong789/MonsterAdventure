using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public partial class SceneLoadManager : GameLifetimeScope, IStartable
{
    [Inject] private readonly ISceneLoadController _sceneLoadController;

    private SceneActiveManager _currentActiveManager;

    private string _sceneName = "";
    private string _sceneNameClose = "";

    public void Start()
    {
        _sceneLoadController.OnToggleSceneCompleted
            .Subscribe(val =>
            {
                if (!val && _sceneName != "")
                {
                    if(_sceneNameClose != "")
                    {
                        StartCoroutine(OnCloseSceneAttitiveCoroutine(_sceneNameClose, _sceneName));
                    }
                    else
                    {
                        StartCoroutine(LoadSceneAdditiveCoroutine(_sceneName));
                    }
                }
            })
            .AddTo(this);

        DontDestroyOnLoad(gameObject);
        Debug.Log("SceneLoadManager Initialized");
    }

    public void StartLoadScene(string sceneName)
    {
        _sceneName = sceneName;
        _sceneLoadController.ToggleLoadScene(false);
    }

    private IEnumerator LoadSceneAdditiveCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => _currentActiveManager != null);
        _currentActiveManager.SetActiveObjects(false);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return op;

        Scene sceneLoaded = SceneManager.GetSceneByName(sceneName);
        foreach(GameObject obj in sceneLoaded.GetRootGameObjects())
        {
            if (obj.TryGetComponent(out SceneActiveManager sceneActiveManager))
            {
                sceneActiveManager.SetActiveObjects(true);
                _onCameraChanged.OnNext(sceneActiveManager.GetCurrentCameraObject());
                _currentActiveManager = sceneActiveManager;
                break;
            }
        }
        yield return new WaitForSeconds(0.5f);

        _sceneLoadController.ToggleLoadScene(true);
        ResetValue();
    }

    public void CloseSceneAttitive(string sceneNameClose, string sceneNameBack)
    {
        _sceneName = sceneNameBack;
        _sceneNameClose = sceneNameClose;
        _sceneLoadController.ToggleLoadScene(false);
    }

    private IEnumerator OnCloseSceneAttitiveCoroutine(string sceneNameClose, string sceneNameBack)
    {
        Scene sceneLoaded = SceneManager.GetSceneByName(sceneNameBack);
        foreach (GameObject obj in sceneLoaded.GetRootGameObjects())
        {
            if (obj.TryGetComponent(out SceneActiveManager sceneActiveManager))
            {
                sceneActiveManager.SetActiveObjects(true);
                _onCameraChanged.OnNext(sceneActiveManager.GetCurrentCameraObject());
                _currentActiveManager = sceneActiveManager;
                break;
            }
        }

        AsyncOperation op = SceneManager.UnloadSceneAsync(sceneNameClose);
        yield return op;

        _sceneLoadController.ToggleLoadScene(true);
        ResetValue();
    }

    public void UpdateCurrentScene(SceneActiveManager sceneActiveManager)
    {
        _currentActiveManager = sceneActiveManager;
    }

    private void ResetValue()
    {
        _sceneName = "";
        _sceneNameClose = "";
    }
}
