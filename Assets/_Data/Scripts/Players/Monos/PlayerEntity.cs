using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CharacterEntity, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
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
