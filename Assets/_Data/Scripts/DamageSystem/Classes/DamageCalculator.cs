using UnityEngine;

public class DamageCalculator
{
    public int Calculate(MonsterModel attacker, MonsterModel defender, SkillModel skillModel)
    {
        float sameTypeBonus = attacker.EElementTypes[0] == skillModel.ElementType ? 1.2f : 0f;
        float typeMultiplier = TypeChart.GetMultiplier(attacker.EElementTypes[0], defender.EElementTypes[0]);

        float damage = attacker.Attack * skillModel.Damage / (attacker.Attack + defender.Defense);
        damage *= sameTypeBonus * typeMultiplier;
        damage = Mathf.Max(damage == 0 ? 0 : 1, damage);
        return (int)damage;
    }
}
