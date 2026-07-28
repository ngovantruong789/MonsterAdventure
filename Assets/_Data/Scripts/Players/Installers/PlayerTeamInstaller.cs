using System;
using UnityEngine;

[Serializable]
public class PlayerTeamInstaller : BaseInstaller, IPlayerTeamIntallerProvider
{
    private PlayerTeamController _playerTeamController;
    public PlayerTeamController PlayerTeamController => _playerTeamController;

    public PlayerTeamModel TeamModel => _playerTeamController.TeamModel;
    public bool CanBattle => _playerTeamController.CanBattle;

    [SerializeField] private MonsterSO _monsterSO;
    [SerializeField] private MonsterSO _monsterSO2;
    [SerializeField] private MonsterSO _monsterSO3;

    public override void Initialize()
    {
        base.Initialize();
        //_teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsterSO2, 30));
        //_teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsterSO, 16));
        //_teamModel.PlayerTeam.Add(MonsterModelFactory.Create(_monsterSO3, 13));
        _playerTeamController = new PlayerTeamController(_monsterSO2, _monsterSO);
    }

    public PlayerTeamModel ClonePlayerTeamModel()
    {
        return _playerTeamController.ClonePlayerTeamModel();
    }
}
