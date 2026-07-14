using System;
using UnityEngine;

[Serializable]
public class SkillConfig
{
    [SerializeField] private SkillSO _skillSO;
    public SkillSO StatType => _skillSO;

    [SerializeField] private int _unlockLevel;
    public int UnlockLevel => _unlockLevel;
}
