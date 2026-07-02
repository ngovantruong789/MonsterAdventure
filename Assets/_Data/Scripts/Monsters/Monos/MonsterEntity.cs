using System.Collections.Generic;
using UnityEngine;

public class MonsterEntity : CharacterEntity
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    
    private IMonsterModelProvider _iMonsterModelProvider;
    public IMonsterModelProvider IMonsterModelProvider => _iMonsterModelProvider;

    public override void Initialize()
    {
        base.Initialize();
        foreach (BaseInstaller installer in installerConfigs)
        {
            installer.Initialize();
            if(installer is IMonsterModelProvider iMonsterModelProvider)
            {
                _iMonsterModelProvider = iMonsterModelProvider;
            }
        }
    }

    [ContextMenu("Add MonsterAttributeInstaller")]
    public void AddMonsterAttributeInstaller() => installerConfigs.Add(new MonsterStatsInstaller());
}