using System.Collections.Generic;

public static class MonsterModelFactory
{
    public static MonsterModel Create(MonsterSO monsterSO, int level)
    {
        List<SkillModel> unlockSkills = CalculateSkill.CalculateUnlockedSkillsPerLevel(monsterSO.Skills, level);
        List<SkillModel> battleSkills = CalculateSkill.CalculateBattleSkills(unlockSkills);

        return new MonsterModel
        {
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

    public static MonsterModel Create(MonsterModel monsterModel)
    {
        return new MonsterModel
        {
            NextEvolve = monsterModel.NextEvolve,
            Health = monsterModel.Health,
            MaxHealth = monsterModel.MaxHealth,
            Attack = monsterModel.Attack,
            Defense = monsterModel.Defense,
            Speed = monsterModel.Speed,
            UnlockedSkills = monsterModel.UnlockedSkills,
            BatlleSkills = monsterModel.BatlleSkills,
            Level = monsterModel.Level,
            MonsterAnimator = monsterModel.MonsterAnimator,
            UIAnimator = monsterModel.UIAnimator,
            MonsterName = monsterModel.MonsterName,
            EElementTypes = monsterModel.EElementTypes,
            Experience = monsterModel.Experience,
            IsDead = monsterModel.IsDead,
        };
    }
}