using UnityEngine;

public class BattleMonsterWorldSpaceView : LifetimeScope
{
    [SerializeField] private Transform _playerMonsterObj;
    [SerializeField] private Transform _opponentMonsterObj;

    private Animator _currentPlayerAnimator;
    private Animator _currentOpponentAnimator;

    public void UpdateMonsterAnimator(bool isPlayer, RuntimeAnimatorController runTimeAnimator)
    {
        Animator animatorObj = GetAnimator(isPlayer);
        if (animatorObj == null) return;

        animatorObj.runtimeAnimatorController = runTimeAnimator;
        animatorObj.CrossFade(Animator.StringToHash("Idle_Attack"), 0, 1);
    }

    private Animator GetAnimator(bool isPlayer)
    {
        if (isPlayer)
        {
            return _currentPlayerAnimator = _playerMonsterObj.Find("Model").GetComponent<Animator>();
        }
        else if(!isPlayer)
        {
            return _currentPlayerAnimator = _opponentMonsterObj.Find("Model").GetComponent<Animator>();
        }
        
        return null;
    }
}
