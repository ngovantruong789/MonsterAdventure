using UnityEngine;

public interface ISpawner
{
    Transform Spawn(ESkillId eSkillId, Vector3 position, bool isActive);
}