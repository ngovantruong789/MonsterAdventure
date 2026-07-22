using System;
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
    public static SkillModel[] CalculateUnlockedSkillsPerLevel(SkillConfig[] skillConfigs, int level)
    {
        SkillModel[] skillModels = new SkillModel[skillConfigs.Length];
        for(int i = 0; i < skillConfigs.Length; i++)
        {
            if (skillConfigs[i].UnlockLevel > level) break;

            skillModels[i] = new SkillModel
            {
                ESkillId = skillConfigs[i].SkillSO.ESkillId,
                Damage = skillConfigs[i].SkillSO.Damage,
                FullName = skillConfigs[i].SkillSO.FullName,
                ElementType = skillConfigs[i].SkillSO.ElementType,
                SkillType = skillConfigs[i].SkillSO.SkillType,
            };
        }

        return skillModels;
    }

    public static SkillModel[] CalculateBattleSkills(SkillModel[] unlockedSkills)
    {
        if(unlockedSkills.Length <= 4) return unlockedSkills;

        SkillModel[] battleSkills = new SkillModel[4];
        Array.Copy(unlockedSkills, battleSkills, 4);
        return battleSkills;
    }
}