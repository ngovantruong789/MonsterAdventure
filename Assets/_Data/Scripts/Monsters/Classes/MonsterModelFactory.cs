using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MonsterModelFactory
{
    public static MonsterModel Create(MonsterSO monsterSO, int level)
    {
        List<SkillModel> unlockSkills = CalculateSkill.CalculateUnlockedSkillsPerLevel(monsterSO.Skills, level).ToList();
        List<SkillModel> battleSkills = CalculateSkill.CalculateBattleSkills(unlockSkills.ToArray()).ToList();

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
            Experience = 0,
            IsDead = false,
        };
    }
}