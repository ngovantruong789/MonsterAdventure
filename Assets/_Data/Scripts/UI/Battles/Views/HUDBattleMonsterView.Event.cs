using System;
using UniRx;

public partial class HUDBattleMonsterView
{
    private Subject<int> _onActiveItem = new();
    public IObservable<int> OnActiveItem => _onActiveItem;

    private Subject<Unit> _onShowSkill = new();
    public IObservable<Unit> OnShowSkill => _onShowSkill;

    private Subject<Unit> _onShowItem = new();
    public IObservable<Unit> OnShowItem => _onShowItem;

    private Subject<Unit> _onShowPlayerTeam = new();
    public IObservable<Unit> OnShowPlayerTeam => _onShowPlayerTeam;

    private Subject<Unit> _onOutBattleEvent = new();
    public IObservable<Unit> OnOutBattle => _onOutBattleEvent;

    private readonly Subject<ActiveAttackViewData> _onActiveAttack = new();
    public IObservable<ActiveAttackViewData> OnActiveAttack => _onActiveAttack;

    private readonly Subject<SwapMonsterViewData> _onSwapMonster = new();
    public IObservable<SwapMonsterViewData> OnSwapMonster => _onSwapMonster;

    private readonly Subject<UpdateMonsterStatCompletedViewData> _onUpdateMonsterStatCompleted = new();
    public IObservable<UpdateMonsterStatCompletedViewData> OnUpdateMonsterStatCompleted => _onUpdateMonsterStatCompleted;
}