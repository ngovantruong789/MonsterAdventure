using System.Collections.Generic;

public class HUDBattleMonsterViewData
{
    public List<MonsterViewData> PlayerTeamDatas { get; set; } = new(6);
    public InventoryViewData RestoreInventoryData { get; set; } = new();
    public InventoryViewData CaptureInventoryData { get; set; } = new();
}