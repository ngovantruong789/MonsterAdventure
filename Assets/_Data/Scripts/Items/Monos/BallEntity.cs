using UnityEngine;

public partial class BallEntity : BaseMonoBehaviour
{
    [SerializeField] private AnimationCurve _trajectoryAnimCurve;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _trajectoryMaxHeight;

    private Vector3 _target;
    private Vector3 _trajectoryStartPoint;
    private float _nextPosXNormalized;
    private bool _isMoveable = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        _trajectoryStartPoint = transform.position;
        _nextPosXNormalized = 0f;
        _isMoveable = true;
    }

    private void Update()
    {
        CheckMovement();
        Moving();
    }

    private void Moving()
    {
        if (!_isMoveable) return;

        Vector3 trajectoryRange = _target - _trajectoryStartPoint;
        float nextPosX = transform.position.x + _moveSpeed * Time.deltaTime;
        _nextPosXNormalized = Mathf.Clamp01((nextPosX - _trajectoryStartPoint.x) / trajectoryRange.x);

        float nextPosYNormalized = _trajectoryAnimCurve.Evaluate(_nextPosXNormalized);
        float baseY = Mathf.Lerp(_trajectoryStartPoint.y, _target.y, _nextPosXNormalized);
        float nextPosY = baseY + nextPosYNormalized * _trajectoryMaxHeight;
        Vector3 newPos = new Vector3(nextPosX, nextPosY);
        transform.position = newPos;
    }

    private void CheckMovement()
    {
        Debug.Log(_nextPosXNormalized);
        if (_nextPosXNormalized >= 1f)
        {
            _isMoveable = false;
        }
    }

    public void SetData(Vector3 target)
    {
        _target = target;
    }
}
