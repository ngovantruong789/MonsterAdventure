using UnityEngine;

public partial class MonsterAnimatorController
{
    private readonly int Empty = Animator.StringToHash("Empty");
    private readonly int Idle_Attack_Left = Animator.StringToHash("Idle_Attack_Left");
    private readonly int Idle_Attack_Right = Animator.StringToHash("Idle_Attack_Right");
    private readonly int Attack_Top_Right = Animator.StringToHash("Atk_Top_Right");
    private readonly int Attack_Bottom_Left = Animator.StringToHash("Atk_Bottom_Left");
    private readonly int Top_Right_Hurt = Animator.StringToHash("Top_Right_Hurt");
    private readonly int Bottom_Left_Hurt = Animator.StringToHash("Bottom_Left_Hurt");
    private readonly int Top_Right_Faint = Animator.StringToHash("Top_Right_Faint");
    private readonly int Bottom_Left_Faint = Animator.StringToHash("Bottom_Left_Faint");
}