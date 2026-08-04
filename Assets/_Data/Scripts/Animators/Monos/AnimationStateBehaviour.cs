using System;
using UnityEngine;

public class AnimationStateBehaviour : StateMachineBehaviour
{
    public static event Action<int, int> StateExited;
    private bool isStateExited;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!stateInfo.loop)
        {
            isStateExited = false;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isStateExited) return;
        if(stateInfo.normalizedTime >= 1 && !stateInfo.loop)
        {
            isStateExited = true;
            StateExited?.Invoke(animator.GetInstanceID(), stateInfo.shortNameHash);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isStateExited) return;
        if (!stateInfo.loop)
        {
            isStateExited = true;
        }
        StateExited?.Invoke(animator.GetInstanceID(), stateInfo.shortNameHash);
    }
}