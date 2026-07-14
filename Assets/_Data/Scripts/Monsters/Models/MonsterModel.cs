using System.Collections.Generic;
using UnityEngine;

public class MonsterModel
{
    public EvolveConfig NextEvolve { get; set; }
    public RuntimeAnimatorController MonsterAnimator { get; set; }
    public RuntimeAnimatorController UIAnimator { get; set; }
    public List<SkillModel> UnlockedSkills { get; set; } = new();
    public List<SkillModel> BatlleSkills { get; set; } = new(4);
    public string MonsterName { get; set; }
    public int Attack { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Defense { get; set; }
    public int Level { get; set; }
    public bool IsDead { get; set; }
    public float Experience { get; set; }
}
