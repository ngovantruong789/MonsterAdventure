public partial class PlayerAnimatorController : CharacterAnimatorController
{
    public void SetMovementState(float value)
    {
        _animator.SetFloat(MoveBlendTreeState, value);
    }
}