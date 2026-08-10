using UnityEngine;

public partial class BattleMonsterController : IBattleMonsterPresenter, IBattleMonsterTurn
{
    private int _currentPlayerMonsterBattleIndex;
    public int CurrentPlayerMonsterBattleIndex { get => _currentPlayerMonsterBattleIndex; set => _currentPlayerMonsterBattleIndex = value; }

    private readonly MonsterModel _opponentModel;
    private readonly PlayerTeamModel _playerTeamModel;
    private readonly DamageCalculator _damageCalculator;
    private SkillModel _skillModel = new();
    private EBattlePhase _eBattlePhase;
    private EMonsterSide _eMonsterSide;
    private int _skillIndexAttack;
    private bool _isEndBattle;

    public BattleMonsterController(BattleModel battleModel, PlayerTeamModel playerTeamModel, DamageCalculator damageCalculator)
    {
        _opponentModel = battleModel.OpponentMonsterModel;
        _playerTeamModel = playerTeamModel;
        _damageCalculator = damageCalculator;
        Debug.Log("BattleMonsterController Initialized");
    }

    public void ChangeTurn(EBattlePhase eBattlePhase)
    {
        _eBattlePhase = eBattlePhase;
        _onTurnChanged.OnNext(_eBattlePhase);

        if(_eBattlePhase == EBattlePhase.OpponentTurn)
        {
            AIOpponentAttack();
        }
    }

    private void PlayAnim()
    {
        HandleStatePhaseChanged(_eMonsterSide, EStatePhase.PlayAnimAttack, _skillModel.ESkillId, false);
    }

    private void PlayVFX()
    {
        HandleStatePhaseChanged(_eMonsterSide, EStatePhase.PlayVFXAttack, _skillModel.ESkillId, false);
    }

    private void ApplyDamage()
    {
        MonsterModel playerModel = GetMonsterModel(EMonsterSide.Player);
        MonsterModel opponentModel = GetMonsterModel(EMonsterSide.Opponent);
        int damage = 0;

        if (_eMonsterSide == EMonsterSide.Player)
        {
            damage = _damageCalculator.Calculate(playerModel, opponentModel, _skillModel);
            opponentModel.Health = Mathf.Max(0, opponentModel.Health - damage);
            opponentModel.IsDead = opponentModel.Health <= 0;
        }
        else
        {
            damage = _damageCalculator.Calculate(opponentModel, playerModel, _skillModel);
            playerModel.Health = Mathf.Max(0, playerModel.Health - damage);
            playerModel.IsDead = playerModel.Health <= 0;
        }

        HandleStatePhaseChanged(_eMonsterSide, EStatePhase.ApplyDamage, _skillModel.ESkillId, false);
    }

    private bool CheckEndBattle()
    {
        if(_opponentModel.IsDead)
        {
            return true;
        }

        foreach(MonsterModel model in _playerTeamModel.PlayerTeam)
        {
            if (!model.IsDead)
            {
                return false;
            }
        }

        return true;
    }

    private void EndPhase()
    {
        _skillModel = null;
        _isEndBattle = CheckEndBattle();
        _skillIndexAttack = -1;

        _onEndBattle.OnNext(_isEndBattle);
        HandleStatePhaseChanged(_eMonsterSide, EStatePhase.End, ESkillId.None, _isEndBattle);
    }

    private void AIOpponentAttack()
    {
        ActiveAttack(EMonsterSide.Opponent, UnityEngine.Random.Range(0, _opponentModel.BatlleSkills.Count));
    }

    public void ActiveAttack(EMonsterSide eMonsterSide, int skillIndex)
    {
        if ((eMonsterSide == EMonsterSide.Player && _eBattlePhase == EBattlePhase.PlayerTurn) || 
            (eMonsterSide == EMonsterSide.Opponent && _eBattlePhase == EBattlePhase.OpponentTurn))
        {
            _eMonsterSide = eMonsterSide;
            _skillIndexAttack = skillIndex;
            _skillModel = GetSkillModel(eMonsterSide, skillIndex);
            PlayAnim();
        }
        else
        {
            Debug.LogError("ActiveAttack Error!!!");
        }
    }

    public void NotifyStateCompleted(EStatePhase eStatePhase)
    {
        switch (eStatePhase)
        {
            case EStatePhase.PlayAnimAttack:
                PlayVFX();
                break;
            case EStatePhase.PlayVFXAttack:
                ApplyDamage();
                break;
            case EStatePhase.ApplyDamage:
                EndPhase();
                break;
            case EStatePhase.End:
                _onNextTurn.OnNext(default);
                break;
        }
    }

    private SkillModel GetSkillModel(EMonsterSide eMonsterSide, int skillIndex)
    {
        MonsterModel playerModel = GetMonsterModel(EMonsterSide.Player);
        MonsterModel opponentModel = GetMonsterModel(EMonsterSide.Opponent);

        return eMonsterSide == EMonsterSide.Player ? 
            playerModel.BatlleSkills[_skillIndexAttack] :
            opponentModel.BatlleSkills[skillIndex];
    }

    private MonsterModel GetMonsterModel(EMonsterSide eMonsterSide)
    {
        return eMonsterSide == EMonsterSide.Player ? 
            _playerTeamModel.PlayerTeam[_currentPlayerMonsterBattleIndex] : _opponentModel;
    }

    private void HandleStatePhaseChanged(EMonsterSide eMonsterSide, EStatePhase eStatePhase, ESkillId eSkillId, bool isEndBattle)
    {
        _onStatePhaseChanged.OnNext(new StatePhaseChangedControllerData(eMonsterSide, eStatePhase, eSkillId, _currentPlayerMonsterBattleIndex, isEndBattle));
    }
}