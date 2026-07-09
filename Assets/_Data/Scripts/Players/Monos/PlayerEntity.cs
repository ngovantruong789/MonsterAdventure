using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEntity : CharacterEntity, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    [SerializeField] private SceneLoadManager _sceneLoadManager;

    private IPlayerTeamModelProvider _iPlayerTeamModelProvider;

    public override void Initialize()
    {
        base.Initialize();
        foreach(BaseInstaller installer in installerConfigs)
        {
            installer.Initialize();
            if(installer is IPlayerTeamModelProvider iPlayerTeamModelProvider)
            {
                _iPlayerTeamModelProvider = iPlayerTeamModelProvider;
            }
        }
    }

    [ContextMenu("Add PlayerMovementInstaller")]
    public void AddPlayerMovementInstaller() => installerConfigs.Add(new PlayerMovementInstaller());

    [ContextMenu("Add PlayerTeamInstaller")]
    public void AddPlayerTeamInstaller() => installerConfigs.Add(new PlayerTeamInstaller());

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MonsterEntity monsterEntity)) return;

        _sceneLoadManager.StartLoadScene("BattleScene", new SceneLoadModel
        {
            BatlleModel = new BattleModel
            {
                OpponentMonsterModel = monsterEntity.IMonsterModelProvider.CurrentMonsterModel,
                PlayerTeamModel = _iPlayerTeamModelProvider.PlayerTeamModel,
            },
        });
    }
}
