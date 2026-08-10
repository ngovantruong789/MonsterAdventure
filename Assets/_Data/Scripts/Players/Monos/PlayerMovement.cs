using UnityEngine;

public class PlayerMovement : BaseMonoBehaviour
{
    [SerializeField] private Rigidbody2D _rd;
    [SerializeField] private float _speed;
    [SerializeField] private float _currentVel;

    private Vector2 _currentDir;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
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
}
