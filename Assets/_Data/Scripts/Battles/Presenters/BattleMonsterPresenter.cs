using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public partial class BattleMonsterPresenter : IDisposable, IStartable
{
    //private readonly BattleModel _battleModel;
    private readonly MonsterModel _opponentModel;
    private readonly PlayerTeamModel _playerTeamModel;
    private readonly BattleMonsterWorldSpaceView _battleMonsterView;
    private readonly HUDBattleMonsterView _hUDBattleMonsterView;
    private readonly BattleManager _battleManager;
    private readonly CompositeDisposable _disposable = new();
    private readonly IBattleMonsterPresenter _iBattleMonsterPresenter;

    private EStatePhase _currentStatePhase;

    public BattleMonsterPresenter(BattleMonsterWorldSpaceView battleMonsterView, 
        HUDBattleMonsterView hUDBattleMonsterView, 
        BattleModel battleModel,
        BattleManager battleManager,
        PlayerTeamModel playerTeamModel,
        IBattleMonsterPresenter iBattleMonsterPresenter)
    {
        //_battleModel = battleModel;
        _battleMonsterView = battleMonsterView;
        _hUDBattleMonsterView = hUDBattleMonsterView;
        _battleManager = battleManager;
        _playerTeamModel = playerTeamModel;
        _opponentModel = battleModel.OpponentMonsterModel;
        _iBattleMonsterPresenter = iBattleMonsterPresenter;
        Debug.Log("BattleMonsterPresenter Initialized");
    }

    public void Start()
    {
        UpdateHUDBattleMonsterViewData(true, true);

        //Opponent
        DeployMonster(EMonsterSide.Opponent, -1);

        //Player
        _hUDBattleMonsterView.UpdateMonsterNumber(_playerTeamModel.PlayerTeam.Count);
        _hUDBattleMonsterView.UpdatePlayerTeamAnimator();
        DeployMonster(EMonsterSide.Player, 0);
        _hUDBattleMonsterView.CurrentMonsterSelectedConstructor();

        _hUDBattleMonsterView.OnShowPlayerTeam
            .Subscribe(_ => ShowPlayerTeam())
            .AddTo(_disposable);

        _hUDBattleMonsterView.OnOutBattle
            .Subscribe(_ => OutBattle())
            .AddTo(_disposable);

        _hUDBattleMonsterView.OnShowSkill
            .Subscribe(_ => ShowSkillBattleMonsterHUD())
            .AddTo(_disposable);

        _hUDBattleMonsterView.OnShowItem
            .Subscribe(_ => ShowItem())
            .AddTo(_disposable);

        _hUDBattleMonsterView.OnSwapMonster
            .Subscribe(val => SwapMonster(val.EMonsterSide, val.MonsterIndex))
            .AddTo(_disposable);

        _hUDBattleMonsterView.OnActiveAttack
            .Subscribe(val => ActiveAttack(val.EMonsterSide, val.SkillIndex))
            .AddTo(_disposable);

        _hUDBattleMonsterView.OnUpdateMonsterStatCompleted
            .Subscribe(val => HandleUpdateStatComplete(val.EMonsterSide, val.EStatType))
            .AddTo(_disposable);

        _iBattleMonsterPresenter.OnStatePhaseChanged
            .Subscribe(val => HandleStatePhaseChange(val))
            .AddTo(_disposable);

        _iBattleMonsterPresenter.OnTurnChanged
            .Subscribe(val => HandleTurn(val))
            .AddTo(_disposable);

        _battleMonsterView.OnAnimationCompletedViewData
            .Subscribe(val => HandleAnimationComplete(val.EMonsterSide, val.EMonsterState))
            .AddTo(_disposable);

        _battleMonsterView.OnVFXCompleted
            .Subscribe(val => HandlePlayVFXComplete(val))
            .AddTo(_disposable);
    }

    private void UpdateHUDBattleMonsterViewData(bool updateUnlockSkills, bool updateBattleSkills)
    {
        HUDBattleMonsterViewData hUDBattleMonsterViewData = new HUDBattleMonsterViewData();
        for (int i = 0; i < _playerTeamModel.PlayerTeam.Count; i++)
        {
            MonsterModel model = _playerTeamModel.PlayerTeam[i];
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
        _battleManager.EndBattle();
    }

    private void SwapMonster(EMonsterSide eMonsterSide, int index)
    {
        DeployMonster(eMonsterSide, index);
    }

    private void DeployMonster(EMonsterSide eMonsterSide, int index)
    {
        if (eMonsterSide == EMonsterSide.Player && _playerTeamModel.PlayerTeam[index] != null)
        {
            _battleMonsterView.UpdateMonsterAnimator(eMonsterSide, _playerTeamModel.PlayerTeam[index].MonsterAnimator);
            _hUDBattleMonsterView.UpdateStatsInforText(eMonsterSide, _playerTeamModel.PlayerTeam[index].MonsterName, _playerTeamModel.PlayerTeam[index].Level);
            _hUDBattleMonsterView.UpdateMonsterStats(eMonsterSide, EStatType.Health, _playerTeamModel.PlayerTeam[index].Health, _playerTeamModel.PlayerTeam[index].MaxHealth);
            _hUDBattleMonsterView.UpdateBattteMonsterSkill(index);
            _iBattleMonsterPresenter.CurrentPlayerMonsterBattleIndex = index;
        }
        else if (eMonsterSide == EMonsterSide.Opponent && _opponentModel != null)
        {
            _battleMonsterView.UpdateMonsterAnimator(eMonsterSide, _opponentModel.MonsterAnimator);
            _hUDBattleMonsterView.UpdateStatsInforText(eMonsterSide, _opponentModel.MonsterName, _opponentModel.Level);
            _hUDBattleMonsterView.UpdateMonsterStats(eMonsterSide, EStatType.Health, _opponentModel.Health, _opponentModel.MaxHealth);
        }
    }

    private void RefreshMonsterHUD(EMonsterSide eMonsterSide, int playerMonsterIndex)
    {
        if (eMonsterSide == EMonsterSide.Player)
        {
            _hUDBattleMonsterView.UpdateStatsInforText(eMonsterSide, _playerTeamModel.PlayerTeam[playerMonsterIndex].MonsterName, _playerTeamModel.PlayerTeam[playerMonsterIndex].Level);
            _hUDBattleMonsterView.UpdateMonsterStats(eMonsterSide, EStatType.Health, _playerTeamModel.PlayerTeam[playerMonsterIndex].Health, _playerTeamModel.PlayerTeam[playerMonsterIndex].MaxHealth);
            UpdateHUDBattleMonsterViewData(false, false);
        }
        else
        {
            _hUDBattleMonsterView.UpdateStatsInforText(eMonsterSide, _opponentModel.MonsterName, _opponentModel.Level);
            _hUDBattleMonsterView.UpdateMonsterStats(eMonsterSide, EStatType.Health, _opponentModel.Health, _opponentModel.MaxHealth);
        }
    }

    private void HandleStatePhaseChange(StatePhaseChangedControllerData data)
    {
        _currentStatePhase = data.EStatePhase;
        switch (data.EStatePhase)
        {
            case EStatePhase.PlayAnimAttack:
                HandlePlayAnimAttackPhase(data.EMonsterSide); 
                break;
            case EStatePhase.PlayVFXAttack:
                HandlePlayVFXPhase(data.EMonsterSide, data.ESkillId);
                break;
            case EStatePhase.ApplyDamage:
                HandleApplyDamagePhaseAsync(data.EMonsterSide, data.CurrentPlayerMonsterBattleIndex);
                break;
            case EStatePhase.End:
                HandleEndPhase(data.EMonsterSide, data.IsEndBattle);
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

    private void HandlePlayAnimAttackPhase(EMonsterSide eMonsterSide)
    {
        _battleMonsterView.PlayCrossFade(eMonsterSide, EMonsterState.Attack, 1, 0f);
    }

    private void HandleAnimationComplete(EMonsterSide eMonsterSide, EMonsterState eMonsterState)
    {
        if(eMonsterState == EMonsterState.Attack)
        {
            _battleMonsterView.PlayCrossFade(eMonsterSide, EMonsterState.IdleAttack, 1, 0f);
            _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.PlayAnimAttack);
        }
        else if(eMonsterState == EMonsterState.Faint)
        {
            _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.End);
        }
    }

    private void HandlePlayVFXPhase(EMonsterSide eMonsterSide, ESkillId eSkillId)
    {
        _battleMonsterView.PlayVFX(eMonsterSide, eSkillId);
    }

    private void HandlePlayVFXComplete(EMonsterSide eMonsterSide)
    {
        _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.PlayVFXAttack);
    }
 
    private void HandleApplyDamagePhaseAsync(EMonsterSide eMonsterSide, int monsterIndex)
    {
        if (eMonsterSide == EMonsterSide.Player)
        {
            RefreshMonsterHUD(EMonsterSide.Opponent, -1);
            _battleMonsterView.PlayCrossFade(EMonsterSide.Opponent, EMonsterState.Hurt, 1, 0);
        }
        else
        {
            RefreshMonsterHUD(EMonsterSide.Player, monsterIndex);
            _battleMonsterView.PlayCrossFade(EMonsterSide.Player, EMonsterState.Hurt, 1, 0);
        }
    }

    private void HandleEndPhase(EMonsterSide eMonsterSide, bool isEndBattle)
    {
        if (_opponentModel.IsDead)
        {
            _battleMonsterView.PlayCrossFade(EMonsterSide.Opponent, EMonsterState.Faint, 1, 0f);
        }
        else if (_playerTeamModel.PlayerTeam[_iBattleMonsterPresenter.CurrentPlayerMonsterBattleIndex].IsDead)
        {
            _battleMonsterView.PlayCrossFade(EMonsterSide.Player, EMonsterState.Faint, 1, 0f);
        }
        else
        {
            _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.End);
        }
    }

    private void ActiveAttack(EMonsterSide eMonsterSide, int skillIndex)
    {
        _iBattleMonsterPresenter.ActiveAttack(eMonsterSide, skillIndex);
    }

    private async void HandleUpdateStatComplete(EMonsterSide eMonsterSide, EStatType eStatType)
    {
        if(eStatType == EStatType.Health && _currentStatePhase == EStatePhase.ApplyDamage)
        {
            _battleMonsterView.PlayCrossFade(eMonsterSide, EMonsterState.IdleAttack, 1, 0f);

            await Task.Delay(500);
            _iBattleMonsterPresenter.NotifyStateCompleted(EStatePhase.ApplyDamage);
        }
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}