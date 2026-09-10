using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public partial class PlayerTeamController : IPlayerTeamProvider, IStartable, IDisposable
{
    private PlayerTeamModel _teamModel;
    public PlayerTeamModel TeamModel => _teamModel;

    public bool CanBattle => CheckCanBattle();

    private IReadOnlyList<MonsterSO> _monsters;
    private IPlayerData _playerDataController;
    private readonly MonsterDatabaseSO _monsterDatabaseSO;
    private readonly CompositeDisposable _disposable = new();

    public PlayerTeamController(MonsterDatabaseSO database, 
        PlayerTeamModel teamModel, 
        IReadOnlyList<MonsterSO> monsters)
    {
        _teamModel = teamModel;
        _monsters = monsters;
        _monsterDatabaseSO = database;
        Debug.Log("PlayerTeamController Initialized");
    }

    public void Start()
    {
        
    }

    private void LoadPlayerTeamData(MonsterTeamDataModel teamDataModel)
    {
        UpdateTeamModel(new PlayerTeamModel
        {
            PlayerTeam = MonsterModelFactory.ConvertTeamDataModelToTeamModel(teamDataModel.BattleMonsters, _monsterDatabaseSO)
        });
    }

    public void PlayerTeamConstructor()
    {
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsters[0], 30));
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsters[1], 16));
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsters[2], 10));
        _onUpdatePlayerTeam.OnNext(default);
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
        _onUpdatePlayerTeam.OnNext(default);
    }

    public void AddMonster(MonsterModel monster)
    {
        _teamModel.PlayerTeam.Add(monster);
        _onUpdatePlayerTeam.OnNext(default);
    }

    public void SetPlayerDataController(IPlayerData playerData)
    {
        _playerDataController = playerData;
        _playerDataController.OnLoadData
            .Subscribe(val => LoadPlayerTeamData(val.MonsterTeamDataModel))
            .AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}