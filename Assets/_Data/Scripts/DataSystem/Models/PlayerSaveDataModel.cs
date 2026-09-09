using Firebase.Firestore;
using System.Collections.Generic;

[FirestoreData]
public class PlayerSaveDataModel
{
    [FirestoreProperty]
    public PlayerDataModel PlayerDataModel { get; set; }

    [FirestoreProperty]
    public InventoryDataModel InventoryDataModel { get; set; }

    [FirestoreProperty]
    public MonsterTeamDataModel MonsterTeamDataModel { get; set; }
}

[FirestoreData]
public class PlayerDataModel
{
    [FirestoreProperty]
    public float PositionX { get; set; }

    [FirestoreProperty]
    public float PositionY { get; set; }

    [FirestoreProperty]
    public float PositionZ { get; set; }

    [FirestoreProperty]
    public int MapType { get; set; }
}

[FirestoreData]
public class InventoryDataModel
{
    [FirestoreProperty]
    public List<ItemDataModel> RestoreItems { get; set; }

    [FirestoreProperty]
    public List<ItemDataModel> CaptureItems { get; set; }
}

[FirestoreData]
public class ItemDataModel
{
    [FirestoreProperty]
    public int ItemID { get; set; }

    [FirestoreProperty]
    public int ItemType { get; set; }

    [FirestoreProperty]
    public int Quantity { get; set; }
}

[FirestoreData]
public class MonsterTeamDataModel
{
    [FirestoreProperty]
    public List<MonsterDataModel> BattleMonsters { get; set; }
}

[FirestoreData]
public class MonsterDataModel
{
    [FirestoreProperty]
    public int MonsterID { get; set; }

    [FirestoreProperty]
    public int Attack { get; set; }

    [FirestoreProperty]
    public int Speed { get; set; }

    [FirestoreProperty]
    public int Defense { get; set; }

    [FirestoreProperty]
    public int Health { get; set; }

    [FirestoreProperty]
    public int MaxHealth { get; set; }

    [FirestoreProperty]
    public int Level { get; set; }

    [FirestoreProperty]
    public float Experience { get; set; }
}