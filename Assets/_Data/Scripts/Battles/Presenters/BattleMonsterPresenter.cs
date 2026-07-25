using System.Collections.Generic;

public class BattleMonsterPresenter
{
    private BattleModel _battleModel;
    private BattleMonsterWorldSpaceView _battleMonsterView;
    private HUDBattleMonsterView _hUDBattleMonsterView;
    private BattleManager _battleManager;
    private IBattleMonsterPresenter _iBattleMonsterPresenter;

    public BattleMonsterPresenter(BattleMonsterWorldSpaceView battleMonsterView, 
        HUDBattleMonsterView hUDBattleMonsterView, 
        BattleModel battleModel,
        BattleManager battleManager,
        IBattleMonsterPresenter iBattleMonsterPresenter)
    {
        _battleModel = battleModel;
        _battleMonsterView = battleMonsterView;
        _hUDBattleMonsterView = hUDBattleMonsterView;
        _battleManager = battleManager;
        _iBattleMonsterPresenter = iBattleMonsterPresenter;

        UpdateHUDBattleMonsterViewData(true, true);

        //Opponent
        DeployMonster(false, -1);

        //Player
        _hUDBattleMonsterView.UpdateMonsterNumber(_battleModel.PlayerTeamModel.PlayerTeam.Count);
        _hUDBattleMonsterView.UpdatePlayerTeamAnimator();
        DeployMonster(true, 0);
        _hUDBattleMonsterView.CurrentMonsterSelectedConstructor();

        _hUDBattleMonsterView.OnShowPlayerTeamEvent += ShowPlayerTeam;
        _hUDBattleMonsterView.OnOutBattleEvent += OutBattle;
        _hUDBattleMonsterView.OnShowSkillsEvent += ShowSkillBattleMonsterHUD;
        _hUDBattleMonsterView.OnShowItemsEvent += ShowItem;
        _hUDBattleMonsterView.OnSwapMonster += SwapMonster;
        _hUDBattleMonsterView.OnActiveAttack += ActiveAttack;
        _iBattleMonsterPresenter.StatePhaseChangeEvt += HandleStatePhaseChange;
        _iBattleMonsterPresenter.TurnEvt += HandleTurn;
    }

    private void UpdateHUDBattleMonsterViewData(bool updateUnlockSkills, bool updateBattleSkills)
    {
        HUDBattleMonsterViewData hUDBattleMonsterViewData = new HUDBattleMonsterViewData();
        for (int i = 0; i < _battleModel.PlayerTeamModel.PlayerTeam.Count; i++)
        {
            MonsterModel model = _battleModel.PlayerTeamModel.PlayerTeam[i];
            MonsterViewData monsterViewData = CovertMonsterModelToMonsterViewData(model);

            monsterViewData.UnlockedSkills = updateUnlockSkills ? ConvertSkillsModelToSkillViewData(model.UnlockedSkills) : _hUDBattleMonsterView.HUDBattleMonsterViewData.PlayerTeamDatas[i].UnlockedSkills;
            monsterViewData.BatlleSkills = updateBattleSkills ? ConvertSkillsModelToSkillViewData(model.BatlleSkills) : _hUDBattleMonsterView.HUDBattleMonsterViewData.PlayerTeamDatas[i].BatlleSkills;
            hUDBattleMonsterViewData.PlayerTeamDatas.Add(monsterViewData);
        }

        _hUDBattleMonsterView.SetData(hUDBattleMonsterViewData);
    }
    private MonsterViewData CovertMonsterModelToMonsterViewData(MonsterModel model)
    {
        return new MonsterViewData
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
            UnlockedSkills = new List<SkillViewData>(),
            BatlleSkills = new List<SkillViewData>()
        };
    }

    private List<SkillViewData> ConvertSkillsModelToSkillViewData(List<SkillModel> originalSkills)
    {
        List<SkillViewData> viewSkills = new List<SkillViewData>();

        foreach (var skill in originalSkills)
        {
            viewSkills.Add(new SkillViewData
            {
                Damage = skill.Damage,
                ElementType = skill.ElementType,
                FullName = skill.FullName,
                ESkillId = skill.ESkillId,
                SkillType = skill.SkillType,
            });
        }

        return viewSkills;
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

    private void SwapMonster(bool isPlayer, int index)
    {
        DeployMonster(isPlayer, index);
    }

    private void DeployMonster(bool isPlayer, int index)
    {
        if (isPlayer && _battleModel.PlayerTeamModel.PlayerTeam[index] != null)
        {
            _battleMonsterView.UpdateMonsterAnimator(true, _battleModel.PlayerTeamModel.PlayerTeam[index].MonsterAnimator);
            _hUDBattleMonsterView.UpdateStatsInforText(true, _battleModel.PlayerTeamModel.PlayerTeam[index].MonsterName, _battleModel.PlayerTeamModel.PlayerTeam[index].Level);
            _hUDBattleMonsterView.UpdateMonsterStats(true, EStatType.Health, _battleModel.PlayerTeamModel.PlayerTeam[index].Health, _battleModel.PlayerTeamModel.PlayerTeam[index].MaxHealth);
            _hUDBattleMonsterView.UpdateBattteMonsterSkill(index);
            _iBattleMonsterPresenter.CurrentPlayerMonsterBattleIndex = index;
        }
        else if (!isPlayer && _battleModel.OpponentMonsterModel != null)
        {
            _battleMonsterView.UpdateMonsterAnimator(false, _battleModel.OpponentMonsterModel.MonsterAnimator);
            _hUDBattleMonsterView.UpdateStatsInforText(false, _battleModel.OpponentMonsterModel.MonsterName, _battleModel.OpponentMonsterModel.Level);
            _hUDBattleMonsterView.UpdateMonsterStats(false, EStatType.Health, _battleModel.OpponentMonsterModel.Health, _battleModel.OpponentMonsterModel.MaxHealth);
        }
    }

    private void RefreshMonsterHUD(bool isPlayer, int playerMonsterIndex)
    {
        if (isPlayer)
        {
            _hUDBattleMonsterView.UpdateStatsInforText(true, _battleModel.PlayerTeamModel.PlayerTeam[playerMonsterIndex].MonsterName, _battleModel.PlayerTeamModel.PlayerTeam[playerMonsterIndex].Level);
            _hUDBattleMonsterView.UpdateMonsterStats(true, EStatType.Health, _battleModel.PlayerTeamModel.PlayerTeam[playerMonsterIndex].Health, _battleModel.PlayerTeamModel.PlayerTeam[playerMonsterIndex].MaxHealth);
            UpdateHUDBattleMonsterViewData(false, false);
        }
        else
        {
            _hUDBattleMonsterView.UpdateStatsInforText(false, _battleModel.OpponentMonsterModel.MonsterName, _battleModel.OpponentMonsterModel.Level);
            _hUDBattleMonsterView.UpdateMonsterStats(false, EStatType.Health, _battleModel.OpponentMonsterModel.Health, _battleModel.OpponentMonsterModel.MaxHealth);
        }
    }

    private void HandleStatePhaseChange(bool isPlayer, EStatePhase eStatePhase, int monsterIndex)
    {
        switch(eStatePhase)
        {
            case EStatePhase.PlayAnimAttack:
                HandlePlayAnimAttackPhase(isPlayer); 
                break;
            case EStatePhase.PlayVFXAttack:
                HandlePlayVFXPhase(isPlayer);
                break;
            case EStatePhase.ApplyDamage:
                HandleApplyDamagePhase(isPlayer, monsterIndex);
                break;
            case EStatePhase.End:
                HandleEndPhase();
                break;
        }
    }

    private void HandleTurn(EBattlePhase eBattlePhase)
    {
        if(eBattlePhase == EBattlePhase.PlayerTurn)
        {
            _hUDBattleMonsterView.IsInteract = true;
        }
        else if(eBattlePhase == EBattlePhase.OpponentTurn)
        {
            _hUDBattleMonsterView.IsInteract = false;
        }
        else if(eBattlePhase == EBattlePhase.End)
        {
            OutBattle();
        }
    }

    private void HandlePlayAnimAttackPhase(bool isPlayer)
    {
        _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.PlayAnimAttack);
    }

    private void HandlePlayVFXPhase(bool isPlayer)
    {
        _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.PlayVFXAttack);
    }
    
    private void HandleApplyDamagePhase(bool isPlayer, int monsterIndex)
    {
        if (isPlayer)
        {
            RefreshMonsterHUD(false, -1);
        }
        else
        {
            RefreshMonsterHUD(true, monsterIndex);
        }
        _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.ApplyDamage);
    }

    private void HandleEndPhase()
    {
        _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.End);
    }

    private void ActiveAttack(bool isPlayer, int skillIndex)
    {
        _iBattleMonsterPresenter.ActiveAttack(isPlayer, skillIndex);
    }
}