using System;
using UnityEngine;

public interface IPlayerMovement
{
    IObservable<bool> IsMoveable { get; }
    void SetMove(bool canMove);
    void ChangePos(Vector2 dir, float speedIntensity);
}