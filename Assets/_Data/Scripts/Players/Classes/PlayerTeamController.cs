public class PlayerTeamController
{
    private PlayerTeamModel _teamModel;
    public PlayerTeamModel TeamModel => _teamModel;

    public bool CanBattle => CheckCanBattle();

    public PlayerTeamController(MonsterSO monsterSO, MonsterSO monsterSO2)
    {
        _teamModel = new PlayerTeamModel();
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(monsterSO, 30));
        _teamModel.PlayerTeam.Add(MonsterModelFactory.Create(monsterSO2, 16));
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

    public PlayerTeamModel ClonePlayerTeamModel()
    {
        PlayerTeamModel playerTeamModel = new PlayerTeamModel();
        foreach (MonsterModel monsterModel in TeamModel.PlayerTeam)
        {
            playerTeamModel.PlayerTeam.Add(MonsterModelFactory.Create(monsterModel));
        }

        return playerTeamModel;
    }
}
