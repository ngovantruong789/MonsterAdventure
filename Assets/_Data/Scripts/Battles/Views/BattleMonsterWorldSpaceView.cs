using System;
using UnityEngine;

public class BattleMonsterWorldSpaceView : LifetimeScope, IStartInit
{
    [SerializeField] private SkillVFXSpawner _skillVFXSpawner;

    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    [SerializeField] private MonsterAnimatorController _playerAnimator;
    [SerializeField] private MonsterAnimatorController _opponentAnimator;

    public Action<EMonsterSide, EMonsterState> AnimationCompletedEvt { get; set; }
    public Action<EMonsterSide> VFXCompletedEvt { get; set; }

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

    public void PlayVFX(EMonsterSide eMonsterSide, ESkillId eSkillId)
    {
        Vector3 pos = eMonsterSide == EMonsterSide.Player ? _opponentMonsterObj.position : _playerMonsterObj.position;
        Transform vfx = _skillVFXSpawner.Spawn(eSkillId, pos);
        if (vfx == null) return;
        if (!vfx.TryGetComponent(out ISkillVFXEntity skillVFXEntity)) return;

        Action handler = null;
        handler = () =>
        {
            skillVFXEntity.PlayVFXCompleted -= handler;
            VFXCompletedEvt?.Invoke(eMonsterSide);
        };
        skillVFXEntity.PlayVFXCompleted += handler;

        vfx.gameObject.SetActive(true);
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
