using UnityEngine;

public class BattleMonsterWorldSpaceView : LifetimeScope
{
    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    [SerializeField] private MonsterAnimatorController _playerAnimator;
    [SerializeField] private MonsterAnimatorController _opponentAnimator;

    public void UpdateMonsterAnimator(bool isPlayer, RuntimeAnimatorController runTimeAnimator)
    {
        MonsterAnimatorController monsterAnimatorController = GetMonsterAnimator(isPlayer);
        monsterAnimatorController.UpdateRuntimeAnimator(runTimeAnimator);
        monsterAnimatorController.EnterBattle(isPlayer);
    }

    private MonsterAnimatorController GetMonsterAnimator(bool isPlayer)
    {
        if (isPlayer)
        {
            return _playerAnimator;
        }
        else
        {
            return _opponentAnimator;
        }
    }
}
