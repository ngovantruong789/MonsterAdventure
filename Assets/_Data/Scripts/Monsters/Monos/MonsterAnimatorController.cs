using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public partial class MonsterAnimatorController : CharacterAnimatorController
{
    [SerializeField] private EMonsterSide _currentMonsterSide;

    private EMonsterState _currentMonsterState;
    private int _currentHash;
    public Animator Animator => _animator;

    protected override void OnEnable()
    {
        base.OnEnable();
        AnimationStateBehaviour.OnStateExited
            .Subscribe(val => OnStateExited(val.InstanceID, val.ShortNameHash))
            .AddTo(this);
    }

    public void UpdateRuntimeAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
    public void PlayCrossFade(EMonsterState eMonsterState, int layer, float fade)
    {
        _currentHash = GetStateHash(eMonsterState);
        _currentMonsterState = eMonsterState;

        if(_currentHash == Idle_Attack_Left)
        {
            _animator.SetInteger("IdleAttackValue", 1);
        }
        else if(_currentHash == Idle_Attack_Right)
        {
            _animator.SetInteger("IdleAttackValue", 2);
        }
        _animator.CrossFade(_currentHash, fade, layer, 0f);
    }

    private async void OnStateExited(int animatorId, int hash)
    {
        if (animatorId != _animator.GetInstanceID()) return;

        if(hash == Attack_Top_Right)
        {
            await Task.Delay(1000);
            _animator.CrossFade(Idle_Attack_Left, 0f, 1, 0f);
        }
        else if(hash == Attack_Bottom_Left)
        {
            await Task.Delay(1000);
            _animator.CrossFade(Idle_Attack_Right, 0f, 1, 0f);
        }
        _onAnimationCompleted.OnNext(new MonsterAnimationCompletedData(_currentMonsterSide, _currentMonsterState));

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