using System;
using UnityEngine;

public class AnimationStateBehaviour : StateMachineBehaviour
{
    public static event Action<int> StateExited;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateExited?.Invoke(stateInfo.shortNameHash);
    }
}