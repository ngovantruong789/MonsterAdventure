using UnityEngine;

public partial class MonsterAnimatorController
{
    private static readonly int Empty = Animator.StringToHash("Empty");
    private static readonly int Idle_Attack_Left = Animator.StringToHash("Idle_Attack_Left");
    private static readonly int Idle_Attack_Right = Animator.StringToHash("Idle_Attack_Right");
    private static readonly int Attack_Top_Right = Animator.StringToHash("Atk_Top_Right");
    private static readonly int Attack_Bottom_Left = Animator.StringToHash("Atk_Bottom_Left");
}