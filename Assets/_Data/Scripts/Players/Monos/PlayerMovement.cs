using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : LifetimeScope
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

        Vector2 velocity = _currentDir * _currentVel;
        _rd.linearVelocity = Vector2.ClampMagnitude(velocity, _speed);
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
