using System;

public interface IItemController
{
    public IObservable<ActiveItemControllerEventData> OnActiveItem { get;}
    void UseItem(int id, EItemType itemType, MonsterModel opponentMonster);
}