using System;
using UniRx;
using UnityEngine;

public partial class BattleMonsterWorldSpaceView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private SkillVFXSpawner _skillVFXSpawner;

    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    [SerializeField] private MonsterAnimatorController _playerAnimator;
    [SerializeField] private MonsterAnimatorController _opponentAnimator;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _playerAnimator.OnAnimationCompleted
            .Subscribe(val => OnAnimationComplete(val.EMonsterSide, val.EMonsterState))
            .AddTo(this);

        _opponentAnimator.OnAnimationCompleted
            .Subscribe(val => OnAnimationComplete(val.EMonsterSide, val.EMonsterState))
            .AddTo(this);
    }

    public void UpdateMonsterAnimator(EMonsterSide eMonsterSide, RuntimeAnimatorController runTimeAnimator)
    {
        MonsterAnimatorController monsterAnimatorController = GetMonsterAnimator(eMonsterSide);
        monsterAnimatorController.UpdateRuntimeAnimator(runTimeAnimator);
        monsterAnimatorController.PlayCrossFade(EMonsterState.IdleAttack, 1, 0);
    }

    public void PlayCrossFade(EMonsterSide eMonsterSide, EMonsterState eMonsterState, int layer, float fade)
    {
        MonsterAnimatorController monsterAnimatorController = GetMonsterAnimator(eMonsterSide);
        monsterAnimatorController.PlayCrossFade(eMonsterState, layer, fade);
    }

    public void PlayVFX(EMonsterSide eMonsterSide, ESkillId eSkillId)
    {
        Vector3 pos = eMonsterSide == EMonsterSide.Player ? _opponentMonsterObj.position : _playerMonsterObj.position;
        Transform vfx = _skillVFXSpawner.Spawn(eSkillId, pos);
        if (vfx == null) return;
        if (!vfx.TryGetComponent(out ISkillVFXEntity skillVFXEntity)) return;

        Vector3 scale = vfx.transform.localScale;
        vfx.transform.localScale = eMonsterSide == EMonsterSide.Opponent ? new Vector3(-scale.x, scale.y) : scale;

        Action handler = null;
        handler = () =>
        {
            skillVFXEntity.PlayVFXCompleted -= handler;
            _onVFXCompleted.OnNext(eMonsterSide);
        };
        skillVFXEntity.PlayVFXCompleted += handler;

        vfx.gameObject.SetActive(true);
    }

    private void OnAnimationComplete(EMonsterSide eMonsterSide, EMonsterState eMonsterState)
    {
        _onAnimationCompletedViewData.OnNext(new AnimationCompletedViewData(eMonsterSide, eMonsterState));
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
