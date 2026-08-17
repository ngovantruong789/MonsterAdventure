using UnityEngine;
using VContainer.Unity;

public partial class PlayerMovement : GameLifetimeScope, IPlayerMovement, IStartable
{
    [SerializeField] private Rigidbody2D _rd;
    [SerializeField] private float _speed;
    [SerializeField] private float _currentVel;

    private Vector2 _currentDir;

    public void Start()
    {
        _isMoveable.Value = true;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (!_isMoveable.Value) return;
        if (_rd == null) return;

        Vector3 curentVelocity = _rd.transform.localScale;
        Vector2 velocity = _currentDir * _currentVel;
        _rd.linearVelocity = Vector2.ClampMagnitude(velocity, _speed);

        if (Mathf.Abs(_rd.linearVelocity.x) < float.Epsilon) return;

        curentVelocity.x = (Mathf.Sign(_rd.linearVelocityX) * Mathf.Abs(curentVelocity.x));
        _rd.transform.localScale = curentVelocity;
    }

    public void ChangePos(Vector2 dir, float speedIntensity)
    {
        if (!_isMoveable.Value) return;
        _currentDir = dir;

        if (speedIntensity <= 0)
        {
            _currentVel = 0;
        }
        else
        {
            _currentVel = speedIntensity * _speed;
        }
    }

    private void HandleToggleMove()
    {
        _currentVel = 0;
        _rd.linearVelocity = new Vector2(0f, 0f);
    }

    public void SetMove(bool canMove)
    {
        _isMoveable.Value = canMove;
        HandleToggleMove();
    }
}