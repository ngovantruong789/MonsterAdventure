public interface IPlayerTeamIntallerProvider
{
    PlayerTeamController PlayerTeamController { get;}
    PlayerTeamModel TeamModel { get;}
    PlayerTeamModel ClonePlayerTeamModel();
    bool CanBattle {  get; }
}
