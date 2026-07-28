using UnityEngine;

public interface IMonsterModelProvider
{
    MonsterModel CurrentMonsterModel { get; }
    MonsterModel CloneCurrentMonsterModel();
}
