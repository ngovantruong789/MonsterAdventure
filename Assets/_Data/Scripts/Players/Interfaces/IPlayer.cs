public interface IPlayer
{
    bool CanBattle { get; }
    IPlayerMovement PlayerMovement { get; }
}