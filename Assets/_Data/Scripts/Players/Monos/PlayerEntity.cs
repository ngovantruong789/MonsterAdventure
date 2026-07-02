using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    [SerializeField] private SceneLoadManager _sceneLoadManager;

    public override void Initialize()
    {
        base.Initialize();
        foreach(BaseInstaller installer in installerConfigs)
        {
            installer.Initialize();
        }
    }

    [ContextMenu("Add PlayerMovementInstaller")]
    public void AddInstaller() => installerConfigs.Add(new PlayerMovementInstaller());

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MonsterEntity monsterEntity)) return;

        _sceneLoadManager.StartLoadScene("BattleScene", new SceneLoadModel
        {
            BatlleModel = new BattleModel
            {
                OpponentMonsterModel = monsterEntity.IMonsterModelProvider.CurrentMonsterModel,
            },
        });
    }
}
