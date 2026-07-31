using System;
using UnityEngine;

public partial class MonsterAnimatorController : CharacterAnimatorController
{
    public Action<EMonsterSide, EMonsterState> AnimationCompletedEvt { get; set; }

    private EMonsterState _currentMonsterState;
    private EMonsterSide _currentMonsterSide;
    private int _currentHash;

    protected override void OnEnable()
    {
        base.OnEnable();
        AnimationStateBehaviour.StateExited += OnStateExited;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        AnimationStateBehaviour.StateExited -= OnStateExited;
    }

    public void UpdateRuntimeAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
    public void PlayCrossFade(EMonsterSide eMonsterSide, EMonsterState eMonsterState, int layer, float fade)
    {
        _currentHash = GetStateHash(eMonsterSide, eMonsterState);
        _currentMonsterState = eMonsterState;
        _currentMonsterSide = eMonsterSide;
        _animator.CrossFade(_currentHash, fade, layer);
    }

    private void OnStateExited(int hash)
    {
        AnimationCompletedEvt?.Invoke(_currentMonsterSide, _currentMonsterState);

        _currentMonsterState = EMonsterState.None;
        _currentMonsterSide = EMonsterSide.None;
        _currentHash = -1;
    }

    private int GetStateHash(EMonsterSide side, EMonsterState state)
    {
        return (side, state) 
        switch
        {
            (EMonsterSide.Player, EMonsterState.IdleAttack) => Idle_Attack_Right,
            (EMonsterSide.Opponent, EMonsterState.IdleAttack) => Idle_Attack_Left,

            (EMonsterSide.Player, EMonsterState.Attack) => Attack_Top_Right,
            (EMonsterSide.Opponent, EMonsterState.Attack) => Attack_Bottom_Left,
            (EMonsterSide.Player, EMonsterState.Hurt) => Top_Right_Hurt,
            (EMonsterSide.Opponent, EMonsterState.Hurt) => Bottom_Left_Hurt,

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}