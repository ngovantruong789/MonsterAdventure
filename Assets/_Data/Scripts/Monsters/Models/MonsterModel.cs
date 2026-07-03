using UnityEngine;

public class MonsterModel
{
    public EvolveConfig NextEvolve { get; set; }
    public RuntimeAnimatorController Animator { get; set; }
    public string MonsterName { get; set; }
    public int Attack { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Defense { get; set; }
    public int Level { get; set; }
}
