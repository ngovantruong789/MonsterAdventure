using System;
using UnityEngine;

public class BattleMonsterController : IBattleMonsterPresenter, IBattleMonsterTurn
{
    public Action<EBattlePhase> TurnEvt { get; set; }
    public Action<EMonsterSide, EStatePhase, ESkillId, int, bool> StatePhaseChangeEvt { get; set; }
    public Action<bool> EndBattleEvt { get; set; }
    public Action NextTurnEvt { get; set; }
    public int CurrentPlayerMonsterBattleIndex { get => _currentPlayerMonsterBattleIndex; set => _currentPlayerMonsterBattleIndex = value; }

    private BattleModel _battleModel;
    private DamageCalculator _damageCalculator;
    private SkillModel _skillModel = new();
    private EBattlePhase _eBattlePhase;
    private EMonsterSide _eMonsterSide;
    private int _skillIndexAttack;
    private int _currentPlayerMonsterBattleIndex;
    private bool _isEndBattle;

    public BattleMonsterController(BattleModel battleModel, DamageCalculator damageCalculator)
    {
        _battleModel = battleModel;
        _damageCalculator = damageCalculator;
    }

    public void ChangeTurn(EBattlePhase eBattlePhase)
    {
        _eBattlePhase = eBattlePhase;
        TurnEvt.Invoke(_eBattlePhase);

        if(_eBattlePhase == EBattlePhase.OpponentTurn)
        {
            AIOpponentAttack();
        }
    }

    private void PlayAnim()
    {
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.PlayAnimAttack, _skillModel.ESkillId, _currentPlayerMonsterBattleIndex, false);
    }

    private void PlayVFX()
    {
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.PlayVFXAttack, _skillModel.ESkillId, _currentPlayerMonsterBattleIndex, false);
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

        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.ApplyDamage, _skillModel.ESkillId, _currentPlayerMonsterBattleIndex, false);
    }

    private bool CheckEndBattle()
    {
        if(_battleModel.OpponentMonsterModel.IsDead)
        {
            return true;
        }

        foreach(MonsterModel model in _battleModel.PlayerTeamModel.PlayerTeam)
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

        EndBattleEvt.Invoke(_isEndBattle);
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.End, ESkillId.None, _currentPlayerMonsterBattleIndex, _isEndBattle);
    }

    private void AIOpponentAttack()
    {
        ActiveAttack(EMonsterSide.Opponent, UnityEngine.Random.Range(0, _battleModel.OpponentMonsterModel.BatlleSkills.Count));
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
                NextTurnEvt.Invoke();
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
            _battleModel.PlayerTeamModel.PlayerTeam[_currentPlayerMonsterBattleIndex] :
            _battleModel.OpponentMonsterModel;
    }
}