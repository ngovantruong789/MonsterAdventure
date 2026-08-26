public interface IPlayerTeamProvider
{
    PlayerTeamModel TeamModel { get;}
    void AddMonster(MonsterModel monster);
    bool CanBattle {  get; }
}