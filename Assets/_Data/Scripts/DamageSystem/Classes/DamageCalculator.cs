using UnityEngine;

public class DamageCalculator
{
    public int Calculate(MonsterModel attacker, MonsterModel defender, SkillModel skillModel)
    {
        float damage = attacker.Attack * skillModel.Damage / (attacker.Attack + defender.Defense);
        damage = Mathf.Max(1, damage);
        return (int)damage;
    }
}
