using Unity.VisualScripting;
using UnityEngine;

public partial class BallEntity : BaseMonoBehaviour
{
    [SerializeField] private AnimationCurve _trajectoryAnimCurve;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _trajectoryMaxHeight;
    [SerializeField] private Animator _animator;

    private Vector3 _target;
    private Vector3 _trajectoryStartPoint;
    private float _nextPosXNormalized;
    private bool _isMoveable = true;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        CheckMovement();
        Moving();
    }

    private void Moving()
    {
        if (!_isMoveable) return;

        float distance = Mathf.Abs(_target.x - _trajectoryStartPoint.x);
        float normalized = Mathf.Clamp01(_nextPosXNormalized + (_moveSpeed * Time.deltaTime) / distance);
        float x = Mathf.Lerp(_trajectoryStartPoint.x,_target.x,normalized);
        float curveY = _trajectoryAnimCurve.Evaluate(normalized);
        float baseY = Mathf.Lerp(_trajectoryStartPoint.y,_target.y,normalized);
        float y = baseY + curveY * _trajectoryMaxHeight;

        transform.position = new Vector3(x, y);
        _nextPosXNormalized = normalized;
    }

    private void CheckMovement()
    {
        if (!_isMoveable) return;
        if (_nextPosXNormalized >= 1f)
        {
            _isMoveable = false;
            _onActivePhaseCompleted.OnNext(EBallState.Throw);
        }
    }

    public void ToggleOpenBall(EBallState ballState, float value)
    {
        _animator.SetInteger("BallState", 1);
        _animator.SetFloat("IdleStateValue", value);
    }

    public void RotateBall(bool value)
    {
        _animator.SetInteger("BallState", 3);
        _animator.SetBool("IsRotate", value);
    }

    public void SetData(Vector3 target, bool isBack)
    {
        _target = target;
        _trajectoryStartPoint = transform.position;
        _nextPosXNormalized = 0f;

        _animator.SetInteger("BallState", 2);
        _isMoveable = true;
    }
}
