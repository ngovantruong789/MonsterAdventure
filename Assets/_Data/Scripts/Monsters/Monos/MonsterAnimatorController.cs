using System;
using UnityEngine;

public partial class MonsterAnimatorController : CharacterAnimatorController
{
    [SerializeField] private EMonsterSide _currentMonsterSide;

    public Action<EMonsterSide, EMonsterState> AnimationCompletedEvt { get; set; }

    private EMonsterState _currentMonsterState;
    private int _currentHash;
    public Animator Animator => _animator;

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
    public void PlayCrossFade(EMonsterState eMonsterState, int layer, float fade)
    {
        _currentHash = GetStateHash(eMonsterState);
        _currentMonsterState = eMonsterState;
        _animator.CrossFade(_currentHash, fade, layer, 0f);
    }

    private void OnStateExited(int animatorId, int hash)
    {
        if (animatorId != _animator.GetInstanceID()) return;

        AnimationCompletedEvt?.Invoke(_currentMonsterSide, _currentMonsterState);

        _currentMonsterState = EMonsterState.None;
        _currentHash = -1;
    }

    public int GetStateHash(EMonsterState state)
    {
        return (_currentMonsterSide, state) 
        switch
        {
            (EMonsterSide.Player, EMonsterState.IdleAttack) => Idle_Attack_Right,
            (EMonsterSide.Opponent, EMonsterState.IdleAttack) => Idle_Attack_Left,

            (EMonsterSide.Player, EMonsterState.Attack) => Attack_Top_Right,
            (EMonsterSide.Opponent, EMonsterState.Attack) => Attack_Bottom_Left,
            (EMonsterSide.Player, EMonsterState.Hurt) => Top_Right_Hurt,
            (EMonsterSide.Opponent, EMonsterState.Hurt) => Bottom_Left_Hurt,
            (EMonsterSide.Player, EMonsterState.Faint) => Top_Right_Faint,
            (EMonsterSide.Opponent, EMonsterState.Faint) => Bottom_Left_Faint,

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}