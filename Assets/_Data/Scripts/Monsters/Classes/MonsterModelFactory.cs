using UnityEngine;

public static class MonsterModelFactory
{
    public static MonsterModel Create(MonsterSO monsterSO, int level)
    {
        return new MonsterModel
        {
            NextEvolve = monsterSO.NextEvolve,
            Health = StatCalculator.CalculateStatPerLevel(monsterSO.Health.GrowthPerLevels, level),
            Attack = StatCalculator.CalculateStatPerLevel(monsterSO.Attack.GrowthPerLevels, level),
            Defense = StatCalculator.CalculateStatPerLevel(monsterSO.Defense.GrowthPerLevels, level),
            Speed = StatCalculator.CalculateStatPerLevel(monsterSO.Speed.GrowthPerLevels, level),
            Level = level,
            Animator = monsterSO.Animator,
        };
    }
}