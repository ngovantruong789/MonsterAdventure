using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private MonsterEntity _monsterEntity;

    public override void Initialize()
    {
        base.Initialize();
        foreach(BaseInstaller installer in installerConfigs)
        {
            installer.Initialize();
        }
    }

    [ContextMenu("Add installer")]
    public void AddInstaller()
    {
        installerConfigs.Clear();
        installerConfigs.Add(new PlayerMovementInstaller());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _sceneLoadManager.StartLoadScene("BattleScene", new SceneLoadModel
        {
            MonsterEntity = _monsterEntity,
        });
    }
}
