#region HUDBattleMonsterView
public readonly struct ActiveAttackViewData
{
    public EMonsterSide EMonsterSide { get; }
    public int SkillIndex { get; }

    public ActiveAttackViewData(EMonsterSide monsterSide, int skillIndex)
    {
        EMonsterSide = monsterSide;
        SkillIndex = skillIndex;
    }
}

public readonly struct SwapMonsterViewData
{
    public EMonsterSide EMonsterSide { get; }
    public int MonsterIndex { get; }

    public SwapMonsterViewData(EMonsterSide monsterSide, int monsterIndex)
    {
        EMonsterSide = monsterSide;
        MonsterIndex = monsterIndex;
    }
}

public readonly struct UpdateMonsterStatCompletedViewData

{
    public EMonsterSide EMonsterSide { get; }
    public EStatType EStatType { get; }

    public UpdateMonsterStatCompletedViewData(EMonsterSide monsterSide, EStatType eStatType)
    {
        EMonsterSide = monsterSide;
        EStatType = eStatType;
    }
}

public readonly struct UseItemHUDViewData
{
    public int ItemId { get; }
    public EItemType ItemType { get; }

    public int MonsterIndex { get; }

    public UseItemHUDViewData(int itemId, EItemType itemType, int monsterIndex = -1)
    {
        ItemId = itemId;
        ItemType = itemType;
        MonsterIndex = monsterIndex;
    }
}
#endregion HUDBattleMonsterView

#region BattleMonsterWorldSpaceView
public readonly struct AnimationCompletedViewData

{
    public EMonsterSide EMonsterSide { get; }
    public EMonsterState EMonsterState { get; }

    public AnimationCompletedViewData(EMonsterSide monsterSide, EMonsterState eMonsterState)
    {
        EMonsterSide = monsterSide;
        EMonsterState = eMonsterState;
    }
}
#endregion BattleMonsterWorldSpaceView

#region BattleMonsterController
public readonly struct StatePhaseChangedControllerData
{
    public EMonsterSide EMonsterSide { get; }
    public EStatePhase EStatePhase { get; }
    public ESkillId ESkillId { get; }
    public int CurrentPlayerMonsterBattleIndex { get; }
    public bool IsEndBattle { get; }

    public StatePhaseChangedControllerData(EMonsterSide monsterSide,
                                            EStatePhase eStatePhase, 
                                            ESkillId eSkillId,
                                            int currentPlayerMonsterBattleIndex,
                                            bool isEndBattle)
    {
        EMonsterSide = monsterSide;
        EStatePhase = eStatePhase;
        ESkillId = eSkillId;
        CurrentPlayerMonsterBattleIndex = currentPlayerMonsterBattleIndex;
        IsEndBattle = isEndBattle;
    }
}
#endregion BattleMonsterController