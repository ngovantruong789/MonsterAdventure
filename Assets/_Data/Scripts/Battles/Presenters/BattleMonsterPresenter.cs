public class BattleMonsterPresenter
{
    private BattleModel _battleModel;
    private BattleMonsterView _battleMonsterView;

    public BattleMonsterPresenter(BattleMonsterView battleMonsterView, BattleModel battleModel)
    {
        _battleModel = battleModel;
        _battleMonsterView = battleMonsterView;

        _battleMonsterView.UpdateMonsterBattle(false, _battleModel.OpponentMonsterModel);
    }
}
