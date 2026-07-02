using UnityEngine;

public class BattleMonsterView : LifetimeScope
{
    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    public void UpdateMonsterBattle(bool isPlayer, MonsterModel monsterModel)
    {
        Animator animator = isPlayer ? GetAnimator(_playerMonsterObj) : GetAnimator(_opponentMonsterObj);
        animator.runtimeAnimatorController = monsterModel.Animator;
        animator.CrossFade(Animator.StringToHash("Idle_Attack"), 0, 1);
    }

    private Animator GetAnimator(Transform obj)
    {
        return obj.Find("Model").GetComponent<Animator>();
    }
}
