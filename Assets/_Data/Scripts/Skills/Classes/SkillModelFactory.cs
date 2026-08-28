using System.Collections.Generic;
using UnityEngine;

public static class SkillModelFactory
{
    public static SkillModel Create(SkillSO skillSO)
    {
        return new SkillModel
        {
            ESkillId = skillSO.ESkillId,
            Damage = skillSO.Damage,
            FullName = skillSO.FullName,
            ElementType = skillSO.ElementType,
            SkillType = skillSO.SkillType,
        };
    }

    public static SkillViewData ConvertSkillModelToSkillViewData(SkillModel skillModel)
    {
        return new SkillViewData
        {
            Damage = skillModel.Damage,
            FullName = skillModel.FullName,
            ElementType = skillModel.ElementType,
            SkillType = skillModel.SkillType,
            ESkillId = skillModel.ESkillId,
        };
    }

    public static List<SkillViewData> ConvertListSkillModelToSkillViewData(List<SkillModel> skillModels)
    {
        List<SkillViewData> skills = new();
        foreach (SkillModel skillModel in skillModels)
        {
            skills.Add(ConvertSkillModelToSkillViewData(skillModel));
        }

        return skills;
    }
}