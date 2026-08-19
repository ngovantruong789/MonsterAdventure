using DG.Tweening;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

public partial class BattleMonsterWorldSpaceView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private SkillVFXSpawner _skillVFXSpawner;

    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    [SerializeField] private MonsterAnimatorController _playerAnimator;
    [SerializeField] private MonsterAnimatorController _opponentAnimator;
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private Transform _holderItem;

    [SerializeField] private float _waitOpenCloseBall;

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

    public void PlayCapture(EItemType itemType, bool isComplete, GameObject prefab)
    {
        Transform monsterBall = Instantiate(prefab.transform, _ballSpawnPoint.position, Quaternion.identity, _holderItem);
        BallEntity monsterBallEntity = monsterBall.GetComponent<BallEntity>();
        monsterBallEntity.SetData(new Vector3(_opponentMonsterObj.position.x, _opponentMonsterObj.position.y + 1f), false);
        monsterBallEntity.gameObject.SetActive(true);

        int throwCount = isComplete ? 1 : 2;
        int currentThrow = 0;

        monsterBallEntity.OnActivePhaseCompleted
            .Take(throwCount)
            .Subscribe(_ =>
            {
                currentThrow++;
                if (currentThrow == 1)
                {
                    StartCoroutine(HandleCaptureCoroutine(monsterBallEntity, isComplete));
                }
                else
                {
                    StartCoroutine(HandleCompleteCaptureFalse(monsterBallEntity));
                }
            })
            .AddTo(this);
    }

    private IEnumerator HandleCaptureCoroutine(BallEntity ballEntity,bool isComplete)
    {
        ballEntity.ToggleOpenBall(EBallState.Idle, 0.5f);
        yield return _waitOpenCloseBall;
        yield return _opponentMonsterObj.DOScale(Vector3.zero, 1f).SetEase(Ease.Linear).WaitForCompletion();

        ballEntity.ToggleOpenBall(EBallState.Idle, 1f);
        yield return _waitOpenCloseBall;

        ballEntity.ToggleOpenBall(EBallState.Idle, 0f);
        yield return _waitOpenCloseBall;
        yield return ballEntity.transform.DOMoveY(ballEntity.transform.position.y - 1f, 1f).SetEase(Ease.Linear).WaitForCompletion();

        int rotateCount = isComplete ? 3 : UnityEngine.Random.Range(1, 3);
        bool rotateDirection = true;

        for (int i = 0; i < rotateCount; i++)
        {
            ballEntity.RotateBall(rotateDirection);
            rotateDirection = !rotateDirection;
            yield return new WaitForSeconds(2);
        }
        if (isComplete)
        {
            _onActiveItemCompleted.OnNext(EItemType.Capture);
            yield break;
        }

        ballEntity.ToggleOpenBall(EBallState.Idle, 0.5f);
        yield return _waitOpenCloseBall;
        yield return _opponentMonsterObj.DOScale(Vector3.one, 1f).SetEase(Ease.Linear).WaitForCompletion();

        ballEntity.ToggleOpenBall(EBallState.Idle, 1f);
        yield return _waitOpenCloseBall;

        ballEntity.SetData(_ballSpawnPoint.position,true);
    }

    private IEnumerator HandleCompleteCaptureFalse(BallEntity ballEntity)
    {
        yield return new WaitForSeconds(1);

        ballEntity.gameObject.SetActive(false);
        _onActiveItemCompleted.OnNext(EItemType.Capture);
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
