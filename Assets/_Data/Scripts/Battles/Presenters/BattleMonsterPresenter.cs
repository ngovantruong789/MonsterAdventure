public class BattleMonsterPresenter
{
    private BattleModel _battleModel;
    private BattleMonsterWorldSpaceView _battleMonsterView;
    private HUDBattleMonsterView _hUDBattleMonsterView;

    public BattleMonsterPresenter(BattleMonsterWorldSpaceView battleMonsterView, HUDBattleMonsterView hUDBattleMonsterView, BattleModel battleModel)
    {
        _battleModel = battleModel;
        _battleMonsterView = battleMonsterView;
        _hUDBattleMonsterView = hUDBattleMonsterView;

        _battleMonsterView.UpdateMonsterAnimator(false, _battleModel.OpponentMonsterModel.Animator);
        _hUDBattleMonsterView.UpdateMonsterName(false, _battleModel.OpponentMonsterModel.MonsterName);
        _hUDBattleMonsterView.UpdateMonsterStats(false, EStatType.Health, _battleModel.OpponentMonsterModel.Health, _battleModel.OpponentMonsterModel.MaxHealth);
    }
}
