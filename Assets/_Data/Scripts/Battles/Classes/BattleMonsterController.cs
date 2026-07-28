using System;
using UnityEngine;

public class BattleMonsterController : IBattleMonsterPresenter, IBattleMonsterTurn
{
    public Action<EBattlePhase> TurnEvt { get; set; }
    public Action<EMonsterSide, EStatePhase, int, bool> StatePhaseChangeEvt { get; set; }
    public Action<bool> EndBattleEvt { get; set; }
    public Action NextTurnEvt { get; set; }
    public int CurrentPlayerMonsterBattleIndex { get => _currentPlayerMonsterBattleIndex; set => _currentPlayerMonsterBattleIndex = value; }

    private BattleModel _battleModel;
    DamageCalculator _damageCalculator;
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
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.PlayAnimAttack, _currentPlayerMonsterBattleIndex, false);
    }

    private void PlayVFX()
    {
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.PlayVFXAttack, _currentPlayerMonsterBattleIndex, false);
    }

    private void ApplyDamage()
    {
        MonsterModel playerModel = _battleModel.PlayerTeamModel.PlayerTeam[_currentPlayerMonsterBattleIndex];
        MonsterModel opponentModel = _battleModel.OpponentMonsterModel;
        SkillModel skillModel = null;
        int damage = 0;

        if (_eMonsterSide == EMonsterSide.Player)
        {
            skillModel = playerModel.BatlleSkills[_skillIndexAttack];
            damage = _damageCalculator.Calculate(playerModel, opponentModel, skillModel);
            opponentModel.Health = Mathf.Max(0, opponentModel.Health - damage);
            opponentModel.IsDead = opponentModel.Health <= 0;
        }
        else
        {
            int countBattleSkill = opponentModel.BatlleSkills.Count;
            skillModel = opponentModel.BatlleSkills[UnityEngine.Random.Range(0, countBattleSkill)];
            damage = _damageCalculator.Calculate(opponentModel, playerModel, skillModel);
            playerModel.Health = Mathf.Max(0, playerModel.Health - damage);
            playerModel.IsDead = playerModel.Health <= 0;
        }
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.ApplyDamage, _currentPlayerMonsterBattleIndex, false);
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
        _isEndBattle = CheckEndBattle();
        EndBattleEvt.Invoke(_isEndBattle);
        StatePhaseChangeEvt.Invoke(_eMonsterSide, EStatePhase.End, _currentPlayerMonsterBattleIndex, _isEndBattle);
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
}