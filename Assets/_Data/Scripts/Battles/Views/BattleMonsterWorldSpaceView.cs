using System;
using UnityEngine;

public class BattleMonsterWorldSpaceView : LifetimeScope, IStartInit
{
    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    [SerializeField] private MonsterAnimatorController _playerAnimator;
    [SerializeField] private MonsterAnimatorController _opponentAnimator;

    public Action<EMonsterSide, EMonsterState> AnimationCompletedEvt;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _playerAnimator.AnimationCompletedEvt += OnAnimationComplete;
        _opponentAnimator.AnimationCompletedEvt += OnAnimationComplete;
    }

    public void UpdateMonsterAnimator(EMonsterSide eMonsterSide, RuntimeAnimatorController runTimeAnimator)
    {
        MonsterAnimatorController monsterAnimatorController = GetMonsterAnimator(eMonsterSide);
        monsterAnimatorController.UpdateRuntimeAnimator(runTimeAnimator);
        monsterAnimatorController.PlayCrossFade(eMonsterSide, EMonsterState.IdleAttack, 1, 0);
    }

    public void PlayCrossFade(EMonsterSide eMonsterSide, EMonsterState eMonsterState, int layer, float fade)
    {
        MonsterAnimatorController monsterAnimatorController = GetMonsterAnimator(eMonsterSide);
        monsterAnimatorController.PlayCrossFade(eMonsterSide, eMonsterState, layer, fade);
    }

    private void OnAnimationComplete(EMonsterSide eMonsterSide, EMonsterState eMonsterState)
    {
        AnimationCompletedEvt?.Invoke(eMonsterSide, eMonsterState);
    }

    private MonsterAnimatorController GetMonsterAnimator(EMonsterSide eMonsterSide)
    {
        if (eMonsterSide == EMonsterSide.Player)
        {
            return _playerAnimator;
        }
        else
        {
            return _opponentAnimator;
        }
    }
}
