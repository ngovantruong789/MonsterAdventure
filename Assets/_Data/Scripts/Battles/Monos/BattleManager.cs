using System.Collections.Generic;
using UnityEngine;

public class BattleManager : LifetimeScope, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private SceneReceiverData _sceneReceiverData;

    private MonsterModel _playerMonsterModel;
    private MonsterModel _opponentMonsterModel;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _sceneReceiverData.SceneLoadedEvent += OnGetDataBattle;
    }

    [ContextMenu("Add BattleInstaller")]
    public void AddBattleInstaller() => installerConfigs.Add(new BattleInstaller());

    private void OnGetDataBattle(SceneLoadModel sceneLoadModel)
    {
        foreach (BaseInstaller installer in installerConfigs)
        {
            if (installer is IBattleProvider provider)
            {
                provider.BattleModel = sceneLoadModel.BatlleModel;
            }

            installer.Initialize();
        }
    }

    public void EndBattle(BattleModel newBattleModel)
    {
        SceneLoadModel sceneLoadModel = new SceneLoadModel();
        sceneLoadModel = _sceneReceiverData.SceneLoadModel;
        sceneLoadModel.BatlleModel = newBattleModel;
        _sceneLoadManager.CloseSceneAttitive("BattleScene", "GamePlay", sceneLoadModel);
    }
}
