using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class PlayerTeamController : IPlayerTeamProvider, IStartable
{
    private PlayerTeamModel _teamModel;
    public PlayerTeamModel TeamModel => _teamModel;

    public bool CanBattle => CheckCanBattle();

    private IReadOnlyList<MonsterSO> _monsters;

    public PlayerTeamController(PlayerTeamModel teamModel, IReadOnlyList<MonsterSO> monsters)
    {
        _teamModel = teamModel;
        _monsters = monsters;
        Debug.Log("PlayerTeamController Initialized");
    }

    public void Start()
    {
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsters[0], 30));
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsters[1], 16));
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsters[2], 10));
        UpdateTeamModel(_teamModel);
    }

    public bool CheckCanBattle()
    {
        foreach (MonsterModel model in _teamModel.PlayerTeam)
        {
            if (!model.IsDead)
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateTeamModel(PlayerTeamModel teamModel)
    {
        _teamModel = teamModel;
    }

    public void AddMonster(MonsterModel monster)
    {
        _teamModel.PlayerTeam.Add(monster);
    }
}
