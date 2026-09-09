using System.Collections.Generic;

public static class MonsterDataFactory
{
    public static MonsterTeamDataModel Create(IPlayerTeamProvider playerTeamProvider)
    {
        return new MonsterTeamDataModel
        {
            BattleMonsters = ConvertMonsterTeamToMonsterDataModel(playerTeamProvider.TeamModel.PlayerTeam)
        };
    }

    private static List<MonsterDataModel> ConvertMonsterTeamToMonsterDataModel(List<MonsterModel> monsterModels)
    {
        List<MonsterDataModel> monsterDataModels = new();
        foreach(MonsterModel monsterModel in monsterModels)
        {
            MonsterDataModel monsterDataModel = new MonsterDataModel
            {
                MonsterID = monsterModel.MonsterId,
                Attack = monsterModel.Attack,
                Defense = monsterModel.Defense,
                Speed = monsterModel.Speed,
                Experience = monsterModel.Experience,
                Health = monsterModel.Health,
                MaxHealth = monsterModel.MaxHealth,
                Level = monsterModel.Level,
            };

            monsterDataModels.Add(monsterDataModel);
        }

        return monsterDataModels;
    }
}
