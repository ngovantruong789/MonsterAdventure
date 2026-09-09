using System.Collections.Generic;

public static class MonsterModelFactory
{
    public static MonsterModel Create(MonsterSO monsterSO, int level)
    {
        List<SkillModel> unlockSkills = CalculateSkill.CalculateUnlockedSkillsPerLevel(monsterSO.Skills, level);
        List<SkillModel> battleSkills = CalculateSkill.CalculateBattleSkills(unlockSkills);

        return new MonsterModel
        {
            MonsterId = monsterSO.Id,
            NextEvolve = monsterSO.NextEvolve,
            Health = StatCalculator.CalculateStatPerLevel(monsterSO.Health.GrowthPerLevels, level),
            MaxHealth = StatCalculator.CalculateStatPerLevel(monsterSO.Health.GrowthPerLevels, level),
            Attack = StatCalculator.CalculateStatPerLevel(monsterSO.Attack.GrowthPerLevels, level),
            Defense = StatCalculator.CalculateStatPerLevel(monsterSO.Defense.GrowthPerLevels, level),
            Speed = StatCalculator.CalculateStatPerLevel(monsterSO.Speed.GrowthPerLevels, level),
            UnlockedSkills = unlockSkills,
            BatlleSkills = battleSkills,
            Level = level,
            MonsterAnimator = monsterSO.MonsterAnimator,
            UIAnimator = monsterSO.UIAnimator,
            MonsterName = monsterSO.name,
            EElementTypes = monsterSO.Elements,
            DifficultCapture = CaptureCalculator.CalculateDiffercultCapturePerLevel(monsterSO.CaptureRateConfig, level),
            Experience = 0,
            IsDead = false,
        };
    }

    public static MonsterViewData ConvertMonsterModelToMonsterViewData(MonsterModel monsterModel)
    {
        return new MonsterViewData
        {
            Attack = monsterModel.Attack,
            Defense = monsterModel.Defense,
            Speed = monsterModel.Speed,
            Experience = monsterModel.Experience,
            IsDead = monsterModel.IsDead,
            Health = monsterModel.Health,
            Level = monsterModel.Level,
            MaxHealth = monsterModel.MaxHealth,
            MonsterName = monsterModel.MonsterName,
            NextEvolve = monsterModel.NextEvolve,
            MonsterAnimator = monsterModel.MonsterAnimator,
            UIAnimator = monsterModel.UIAnimator,
            BatlleSkills = SkillModelFactory.ConvertListSkillModelToSkillViewData(monsterModel.BatlleSkills),
            UnlockedSkills = SkillModelFactory.ConvertListSkillModelToSkillViewData(monsterModel.UnlockedSkills),
        };
    }

    public static List<MonsterViewData> ConvertListMonsterModelToMonsterViewData(List<MonsterModel> monsterModels)
    {
        List<MonsterViewData> monsterViewDatas = new();
        foreach(MonsterModel monsterModel in monsterModels)
        {
            monsterViewDatas.Add(ConvertMonsterModelToMonsterViewData(monsterModel));
        }
        return monsterViewDatas;
    }

    public static List<MonsterModel> ConvertTeamDataModelToTeamModel(List<MonsterDataModel> monsterDataModels, MonsterDatabaseSO monsterDatabaseSO)
    {
        List<MonsterModel> monsterModels = new();
        foreach (MonsterDataModel monsterDataModel in monsterDataModels)
        {
            MonsterModel monsterModel = GetMonsterModelFromDatabase(monsterDatabaseSO, monsterDataModel.MonsterID, monsterDataModel.Level);
            if (monsterModel == null) continue;

            monsterModel.Attack = monsterDataModel.Attack;
            monsterDataModel.Health = monsterDataModel.Health;
            monsterModel.MaxHealth = monsterDataModel.MaxHealth;
            monsterModel.Experience = monsterDataModel.Experience;
            monsterModel.Speed = monsterDataModel.Speed;
            monsterModel.Defense = monsterDataModel.Defense;
            monsterModel.Level = monsterDataModel.Level;
            monsterModels.Add(monsterModel);
        }

        return monsterModels;
    }

    private static MonsterModel GetMonsterModelFromDatabase(MonsterDatabaseSO database, int id, int level)
    {
        foreach(MonsterSO monsterSO in database.Monsters)
        {
            if(monsterSO.Id == id)
            {
                return Create(monsterSO, level);
            }
        }

        return null;
    }
}