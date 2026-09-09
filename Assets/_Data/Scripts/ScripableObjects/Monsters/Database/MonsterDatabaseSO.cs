using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDatabaseSO", menuName = "ScriptableObjects/MonsterDatabase")]
public class MonsterDatabaseSO : ScriptableObject
{
    [SerializeField] private List<MonsterSO> monsters = new();
    public List<MonsterSO> Monsters => monsters;
}