using UnityEngine;

public partial class MonsterAnimatorController : CharacterAnimatorController
{
    public void EnterBattle(bool isPlayer) => _animator.CrossFade(isPlayer ? Idle_Attack_Right : Idle_Attack_Left, 0, 1);
    public void UpdateRuntimeAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}