using System.Collections.Generic;
using UnityEngine;

public class BattleManager : LifetimeScope, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private SceneReceiverData _sceneReceiverData;

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
        sceneLoadModel.PlayerTeamModel = newBattleModel.PlayerTeamModel;
        sceneLoadModel.BatlleModel.OpponentMonsterModel = null;
        _sceneLoadManager.CloseSceneAttitive("BattleScene", "GamePlay", sceneLoadModel);
    }
}
