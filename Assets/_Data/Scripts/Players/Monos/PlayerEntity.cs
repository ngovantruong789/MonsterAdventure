using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();

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
}
