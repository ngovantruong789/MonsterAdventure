public class BattleTurnController
{
    private IBattleMonsterTurn _battleMonsterTurn;
    private EBattlePhase _eBattlePhase;
    private bool _isEndBattle;

    public BattleTurnController(IBattleMonsterTurn battleMonsterTurn)
    {
        _eBattlePhase = EBattlePhase.Start;
        _battleMonsterTurn = battleMonsterTurn;

        battleMonsterTurn.EndBattleEvt += SetEndBattle;
        _battleMonsterTurn.NextTurnEvt += HandleNextTurn;

        HandleNextTurn();
    }

    private void HandleNextTurn()
    {
        if (_isEndBattle)
        {
            _eBattlePhase = EBattlePhase.End;
        }
        else if (_eBattlePhase == EBattlePhase.PlayerTurn)
        {
            _eBattlePhase = EBattlePhase.OpponentTurn;
        }
        else
        {
            _eBattlePhase = EBattlePhase.PlayerTurn;
        }
        _battleMonsterTurn.ChangeTurn(_eBattlePhase);
    }

    private void SetEndBattle(bool isEndBattle)
    {
        _isEndBattle = isEndBattle;
    }
}