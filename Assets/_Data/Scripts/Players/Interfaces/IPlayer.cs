using UnityEngine;

public interface IPlayer
{
    Vector3 Position { get; }
    bool CanBattle { get; }
    IPlayerMovement PlayerMovement { get; }
}