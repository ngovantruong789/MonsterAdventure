using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillConfig
{
    [SerializeField] private SkillSO _skillSO;
    public SkillSO SkillSO => _skillSO;

    [SerializeField] private int _unlockLevel;
    public int UnlockLevel => _unlockLevel;
}

public static class CalculateSkill
{
    public static List<SkillModel> CalculateUnlockedSkillsPerLevel(SkillConfig[] skillConfigs, int level)
    {
        List<SkillModel> skillModels = new();
        for(int i = 0; i < skillConfigs.Length; i++)
        {
            if (skillConfigs[i].UnlockLevel > level) break;

            skillModels.Add(SkillModelFactory.Create(skillConfigs[i].SkillSO));
        }

        return skillModels;
    }

    public static List<SkillModel> CalculateBattleSkills(List<SkillModel> unlockedSkills)
    {
        return unlockedSkills.Count <= 4 ? unlockedSkills : unlockedSkills.GetRange(0, 4);
    }
}