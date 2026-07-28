using System;

public interface IBattleMonsterTurn
{
    Action NextTurnEvt { get; set; }
    Action<bool> EndBattleEvt { get; set; }
    void ChangeTurn(EBattlePhase eBattlePhase);
}
