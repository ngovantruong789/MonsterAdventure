using UnityEngine;

public class BattleManager : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private SceneLoadManager _sceneLoadManager;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _sceneLoadManager = FindAnyObjectByType<SceneLoadManager>();
    }

    public void EndBattle()
    {
        _sceneLoadManager.CloseSceneAttitive("BattleScene", "GamePlay");
    }
}
