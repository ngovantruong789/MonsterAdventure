using System;
using UniRx;

public partial class BattleManager
{
    private ReactiveProperty<bool> _onBattleStatus = new();
    public IObservable<bool> OnBattleStatus => _onBattleStatus;
}