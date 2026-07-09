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

        UpdateHUDBattleMonsterViewData();
        _battleMonsterView.UpdateMonsterAnimator(false, _battleModel.OpponentMonsterModel.MonsterAnimator);
        _hUDBattleMonsterView.UpdateMonsterName(false, _battleModel.OpponentMonsterModel.MonsterName);
        _hUDBattleMonsterView.UpdateMonsterStats(false, EStatType.Health, _battleModel.OpponentMonsterModel.Health, _battleModel.OpponentMonsterModel.MaxHealth);
        _hUDBattleMonsterView.UpdateMonsterNumber(_battleModel.PlayerTeamModel.PlayerTeam.Count);
        _hUDBattleMonsterView.UpdatePlayerTeamAnimator();

        _hUDBattleMonsterView.OnShowPlayerTeamEvent += ShowPlayerTeam;
    }

    private void UpdateHUDBattleMonsterViewData()
    {
        HUDBattleMonsterViewData hUDBattleMonsterViewData = new HUDBattleMonsterViewData();
        foreach (MonsterModel model in _battleModel.PlayerTeamModel.PlayerTeam)
        {
            MonsterViewData monsterViewData = new MonsterViewData
            {
                NextEvolve = model.NextEvolve,
                MonsterAnimator = model.MonsterAnimator,
                UIAnimator = model.UIAnimator,
                Health = model.Health,
                MaxHealth = model.MaxHealth,
                Attack = model.Attack,
                Speed = model.Speed,
                IsDead = model.IsDead,
                Experience = model.Experience,
                Defense = model.Defense,
                Level = model.Level,
                MonsterName = model.MonsterName,
            };
            hUDBattleMonsterViewData.PlayerTeamDatas.Add(monsterViewData);
        }

        _hUDBattleMonsterView.SetData(hUDBattleMonsterViewData);
    }

    private void ShowPlayerTeam()
    {
        _hUDBattleMonsterView.ShowPlayerTeam();
    }
}
