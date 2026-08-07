using System;
using UniRx;

public class BattleTurnController : IDisposable
{
    private IBattleMonsterTurn _battleMonsterTurn;
    private EBattlePhase _eBattlePhase;
    private bool _isEndBattle;
    private readonly CompositeDisposable _disposable = new();

    public BattleTurnController(IBattleMonsterTurn battleMonsterTurn)
    {
        _eBattlePhase = EBattlePhase.Start;
        _battleMonsterTurn = battleMonsterTurn;

        battleMonsterTurn.OnEndBattle
            .Subscribe(val => SetEndBattle(val))
            .AddTo(_disposable);

        _battleMonsterTurn.OnNextTurn
            .Subscribe(_ => HandleNextTurn())
            .AddTo(_disposable);

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

    public void Dispose()
    {
        _disposable.Dispose();
    }
}