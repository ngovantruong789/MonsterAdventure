using System;
using UnityEngine;

public class BattleMonsterController : IBattleMonsterPresenter, IBattleMonsterTurn
{
    public Action<EBattlePhase> TurnEvt { get; set; }
    public Action<bool, EStatePhase, int> StatePhaseChangeEvt { get; set; }
    public Action NextTurnEvt { get; set; }
    public int CurrentPlayerMonsterBattleIndex { get => _currentPlayerMonsterBattleIndex; set => _currentPlayerMonsterBattleIndex = value; }
    public bool IsEndBattle { get; set; }

    private BattleModel _battleModel;
    DamageCalculator _damageCalculator;
    private EBattlePhase _eBattlePhase;
    private int _skillIndexAttack;
    private int _currentPlayerMonsterBattleIndex;
    private bool _isPlayerAttack;

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
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.PlayAnimAttack, _currentPlayerMonsterBattleIndex);
    }

    private void PlayVFX()
    {
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.PlayVFXAttack, _currentPlayerMonsterBattleIndex);
    }

    private void ApplyDamage()
    {
        MonsterModel playerModel = _battleModel.PlayerTeamModel.PlayerTeam[_currentPlayerMonsterBattleIndex];
        MonsterModel opponentModel = _battleModel.OpponentMonsterModel;
        SkillModel skillModel = null;
        int damage = 0;

        if (_isPlayerAttack)
        {
            skillModel = playerModel.BatlleSkills[_skillIndexAttack];
            damage = _damageCalculator.Calculate(playerModel, opponentModel, skillModel);
            opponentModel.Health = Mathf.Max(0, opponentModel.Health - damage);
        }
        else
        {
            int countBattleSkill = opponentModel.BatlleSkills.Count;
            skillModel = opponentModel.BatlleSkills[UnityEngine.Random.Range(0, countBattleSkill)];
            damage = _damageCalculator.Calculate(opponentModel, playerModel, skillModel);
            playerModel.Health = Mathf.Max(0, playerModel.Health - damage);
        }
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.ApplyDamage, _currentPlayerMonsterBattleIndex);
    }

    private void EndPhase()
    {
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.End, _currentPlayerMonsterBattleIndex);
    }

    private void AIOpponentAttack()
    {
        ActiveAttack(false, UnityEngine.Random.Range(0, _battleModel.OpponentMonsterModel.BatlleSkills.Count));
    }

    public void ActiveAttack(bool isPlayer, int skillIndex)
    {
        if ((isPlayer && _eBattlePhase == EBattlePhase.PlayerTurn) || (!isPlayer && _eBattlePhase == EBattlePhase.OpponentTurn))
        {
            _isPlayerAttack = isPlayer;
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