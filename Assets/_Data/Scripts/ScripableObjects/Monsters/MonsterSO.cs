using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSO", menuName = "ScripableObjects/Monster")]
public class MonsterSO : ScriptableObject
{
    [SerializeField] private int _id;
    public int Id => _id;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private RuntimeAnimatorController _animator;
    public RuntimeAnimatorController Animator => _animator;

    [SerializeField] private EvolveConfig _nextEvolve;
    public EvolveConfig NextEvolve => _nextEvolve;

    [SerializeField] private CaptureRateConfig _captureRateConfig;
    public CaptureRateConfig CaptureRateConfig => _captureRateConfig;

    [SerializeField] private StatConfig _health = new StatConfig { StatType = EStatType.Health};
    public StatConfig Health => _health;

    [SerializeField] private StatConfig _attack = new StatConfig { StatType = EStatType.Attack };
    public StatConfig Attack => _attack;

    [SerializeField] private StatConfig _defense = new StatConfig { StatType = EStatType.Defense };
    public StatConfig Defense => _defense;

    [SerializeField] private StatConfig _speed = new StatConfig { StatType = EStatType.Speed };
    public StatConfig Speed => _speed;

    [SerializeField] private MonsterMapConfig[] _map;
    public MonsterMapConfig[] Map => _map;
}

[Serializable]
public class EvolveConfig
{
    [SerializeField] private MonsterSO _nextEvolveSO;
    public MonsterSO NextEvolveSO => _nextEvolveSO;

    [SerializeField] private int _levelEvolve;
    public int LevelEvolve => _levelEvolve;
}