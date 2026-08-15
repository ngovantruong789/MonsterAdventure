using System.Collections.Generic;

public class HUDBattleMonsterViewData
{
    public List<MonsterViewData> PlayerTeamDatas { get; set; } = new(6);
    public RestoreInventoryModel RestoreInventoryModel { get; set; } = new();
    public CaptureInventoryModel CaptureInventoryModel { get; set; } = new();
}