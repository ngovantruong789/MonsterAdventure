using UnityEngine;

public class DamageCalculator
{
    public int Calculate(MonsterModel attacker, MonsterModel defender, SkillModel skillModel)
    {
        float sameTypeBonus = CheckSameType(attacker.EElementTypes, skillModel.ElementType) ? 1.2f : 1f;
        float typeMultiplier = GetTypeEffectiveness(attacker.EElementTypes, defender.EElementTypes);
        float damage = attacker.Attack * skillModel.Damage / (attacker.Attack + defender.Defense);

        damage *= sameTypeBonus;
        damage *= typeMultiplier;
        damage = Mathf.Max(0, damage);
        return (int)damage;
    }

    private float GetTypeEffectiveness(EElementType[] attackerElements, EElementType[] defenderElements)
    {
        float result = 1f;
        for (int i = 0; i < attackerElements.Length; i++)
        {
            for(int j = 0; j < defenderElements.Length; j++)
            {
                float multiplier = TypeChart.GetMultiplier(attackerElements[i], defenderElements[j]);
                result *= multiplier;
                if(result == 0f)
                {
                    return 0;
                }
            }
        }
        return result;
    }

    private bool CheckSameType(EElementType[] elements, EElementType skillType)
    {
        foreach(EElementType elem in elements)
            if(elem == skillType) return true;

        return false;
    }
}