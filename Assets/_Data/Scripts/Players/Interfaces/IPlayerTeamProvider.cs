public interface IPlayerTeamProvider
{
    PlayerTeamModel TeamModel { get;}
    PlayerTeamModel ClonePlayerTeamModel();
    bool CanBattle {  get; }
}