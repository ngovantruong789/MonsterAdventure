using System;
using UniRx;

public partial class BattleMonsterController
{
    private Subject<EBattlePhase> _onTurnChanged = new();
    public IObservable<EBattlePhase> OnTurnChanged => _onTurnChanged;

    private Subject<StatePhaseChangedControllerData> _onStatePhaseChanged = new();
    public IObservable<StatePhaseChangedControllerData> OnStatePhaseChanged => _onStatePhaseChanged;

    private Subject<bool> _onEndBattle = new();
    public IObservable<bool> OnEndBattle => _onEndBattle;

    private Subject<Unit> _onNextTurn = new();
    public IObservable<Unit> OnNextTurn => _onNextTurn;
}