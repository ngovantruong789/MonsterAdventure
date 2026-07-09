using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerTeamInstaller : BaseInstaller, IPlayerTeamModelProvider
{
    private PlayerTeamModel _teamModel = new();

    public PlayerTeamModel PlayerTeamModel { get => _teamModel; set => _teamModel = value; }

    [SerializeField] private MonsterSO _monsterSO;
    [SerializeField] private MonsterSO _monsterSO2;

    public override void Initialize()
    {
        base.Initialize();
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsterSO, 5));
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsterSO2, 6));
    }
}
