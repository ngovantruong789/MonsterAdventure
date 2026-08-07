#region MonsterAnimatorController
public readonly struct MonsterAnimationCompletedData
{
    public EMonsterSide EMonsterSide { get; }
    public EMonsterState EMonsterState { get; }

    public MonsterAnimationCompletedData(EMonsterSide eMonsterSide, EMonsterState eMonsterState)
    {
        EMonsterSide = eMonsterSide;
        EMonsterState = eMonsterState;
    }
}
#endregion MonsterAnimatorController