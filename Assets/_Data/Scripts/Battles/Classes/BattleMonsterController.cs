using System;
using UnityEngine;

public class BattleMonsterController : IBattleMonsterPresenter, IBattleMonsterTurn
{
    public Action<EBattlePhase> TurnEvt { get; set; }
    public Action<bool, EStatePhase, int> StatePhaseChangeEvt { get; set; }
    public Action NextTurnEvt { get; set; }
    public bool IsEndBattle { get; set; }

    private BattleModel _battleModel;
    DamageCalculator _damageCalculator;
    private EBattlePhase _eBattlePhase;
    private int _skillIndexAttack;
    private int _monsterIndexAttack;
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
    }

    private void PlayAnim()
    {
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.PlayAnimAttack, _monsterIndexAttack);
    }

    private void PlayVFX()
    {
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.PlayVFXAttack, _monsterIndexAttack);
    }

    private void ApplyDamage()
    {
        MonsterModel playerModel = _battleModel.PlayerTeamModel.PlayerTeam[_monsterIndexAttack];
        MonsterModel opponentModel = _battleModel.OpponentMonsterModel;
        SkillModel skillModel = playerModel.BatlleSkills[_skillIndexAttack];
        int damage = 0;

        if (_isPlayerAttack)
        {
            damage = _damageCalculator.Calculate(playerModel, opponentModel, skillModel);
            opponentModel.Health = Mathf.Max(0, opponentModel.Health - damage);
        }
        else
        {
            damage = _damageCalculator.Calculate(opponentModel, playerModel, skillModel);
            playerModel.Health = Mathf.Max(0, playerModel.Health - damage);
        }
        StatePhaseChangeEvt.Invoke(_isPlayerAttack, EStatePhase.ApplyDamage, _monsterIndexAttack);
    }

    public void ActiveAttack(bool isPlayer, int monsterIndex, int skillIndex)
    {
        if(isPlayer && _eBattlePhase == EBattlePhase.PlayerTurn)
        {
            _isPlayerAttack = isPlayer;
            _monsterIndexAttack = monsterIndex;
            _skillIndexAttack = skillIndex;
            PlayAnim();
        }
        else if(!isPlayer && _eBattlePhase == EBattlePhase.OpponentTurn) 
        {

        }
        else
        {
            Debug.LogError("ActiveSkill Error!!!");
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
                IsEndBattle = true;
                NextTurnEvt.Invoke();
                break;
        }
    }
}
