public class BattleMonsterPresenter
{
    private BattleModel _battleModel;
    private BattleMonsterWorldSpaceView _battleMonsterView;
    private HUDBattleMonsterView _hUDBattleMonsterView;
    private BattleManager _battleManager;

    public BattleMonsterPresenter(BattleMonsterWorldSpaceView battleMonsterView, 
        HUDBattleMonsterView hUDBattleMonsterView, 
        BattleModel battleModel,
        BattleManager battleManager)
    {
        _battleModel = battleModel;
        _battleMonsterView = battleMonsterView;
        _hUDBattleMonsterView = hUDBattleMonsterView;
        _battleManager = battleManager;

        UpdateHUDBattleMonsterViewData();

        //Opponent
        _battleMonsterView.UpdateMonsterAnimator(false, _battleModel.OpponentMonsterModel.MonsterAnimator);
        _hUDBattleMonsterView.UpdateStaticInforText(false, _battleModel.OpponentMonsterModel.MonsterName, _battleModel.OpponentMonsterModel.Level);
        _hUDBattleMonsterView.UpdateMonsterStats(false, EStatType.Health, _battleModel.OpponentMonsterModel.Health, _battleModel.OpponentMonsterModel.MaxHealth);
        
        //Player
        _battleMonsterView.UpdateMonsterAnimator(true, _battleModel.PlayerTeamModel.PlayerTeam[0].MonsterAnimator);
        _hUDBattleMonsterView.UpdateStaticInforText(true, _battleModel.PlayerTeamModel.PlayerTeam[0].MonsterName, _battleModel.PlayerTeamModel.PlayerTeam[0].Level);
        _hUDBattleMonsterView.UpdateMonsterStats(true, EStatType.Health, _battleModel.PlayerTeamModel.PlayerTeam[0].Health, _battleModel.PlayerTeamModel.PlayerTeam[0].MaxHealth);
        _hUDBattleMonsterView.UpdateMonsterNumber(_battleModel.PlayerTeamModel.PlayerTeam.Count);
        _hUDBattleMonsterView.UpdatePlayerTeamAnimator();
        _hUDBattleMonsterView.UpdateBattteMonsterSkill(0);

        _hUDBattleMonsterView.OnShowPlayerTeamEvent += ShowPlayerTeam;
        _hUDBattleMonsterView.OnOutBattleEvent += OutBattle;
        _hUDBattleMonsterView.OnShowSkillsEvent += ShowSkillBattleMonsterHUD;
        _hUDBattleMonsterView.OnShowItemsEvent += ShowItem;
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

            for(int i = 0; i < model.UnlockedSkills.Count; i++)
            {
                SkillViewData skillViewData = new SkillViewData
                {
                    Damage = model.UnlockedSkills[i].Damage,
                    ElementType = model.UnlockedSkills[i].ElementType,
                    FullName = model.UnlockedSkills[i].FullName,
                    Id = model.UnlockedSkills[i].Id,
                    SkillType = model.UnlockedSkills[i].SkillType,
                };
                monsterViewData.UnlockedSkills.Add(skillViewData);
            }
            hUDBattleMonsterViewData.PlayerTeamDatas.Add(monsterViewData);
        }

        _hUDBattleMonsterView.SetData(hUDBattleMonsterViewData);
    }

    private void ShowPlayerTeam()
    {
        _hUDBattleMonsterView.ShowPlayerTeam();
    }

    private void ShowSkillBattleMonsterHUD()
    {
        _hUDBattleMonsterView.ShowSkillBattleMonster();
    }
     private void ShowItem()
    {
        _hUDBattleMonsterView.ShowItem();
    }
    private void OutBattle()
    {
        _battleManager.EndBattle(_battleModel);
    }
}
