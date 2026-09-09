using System;
using UniRx;

public interface IPlayerTeamProvider
{
    IObservable<Unit> OnUpdatePlayerTeam { get; }
    PlayerTeamModel TeamModel { get;}
    void AddMonster(MonsterModel monster);
    void PlayerTeamConstructor();
    void SetPlayerDataController(IPlayerData playerData);
    bool CanBattle {  get; }
}