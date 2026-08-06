public class SceneLoadModel : SceneData
{
    public BattleModel BatlleModel { get; set; }//Necessary data when start battle 
    public PlayerTeamModel PlayerTeamModel { get; set; }//Base data when play game

    public BattleInventoryModel BattleInventoryModel { get; set; }
}
