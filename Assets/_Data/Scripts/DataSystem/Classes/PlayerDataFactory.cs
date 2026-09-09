using UnityEngine;

public static class PlayerDataFactory
{
    public static PlayerDataModel Create(Vector3 playerPos, EMapType mapType)
    {
        return new PlayerDataModel
        {
            PositionX = playerPos.x,
            PositionY = playerPos.y,
            PositionZ = playerPos.z,
            MapType = (int)mapType,
        };
    }
}