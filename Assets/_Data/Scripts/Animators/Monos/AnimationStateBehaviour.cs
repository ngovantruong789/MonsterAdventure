using System;
using UnityEngine;

public partial class AnimationStateBehaviour : StateMachineBehaviour
{
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
            ActiveStateExitedEvent(animator.GetInstanceID(), stateInfo.shortNameHash);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isStateExited) return;
        if (!stateInfo.loop)
        {
            isStateExited = true;
        }
        ActiveStateExitedEvent(animator.GetInstanceID(), stateInfo.shortNameHash);
    }

    private void ActiveStateExitedEvent(int instanceId, int shortNameHash)
    {
        _onStateExited.OnNext(new StateExitedData(instanceId, shortNameHash));
    }
}