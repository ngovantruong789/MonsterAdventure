using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillVFXSO", menuName = "ScriptableObjects/SkillVFXSO")]
public class SkillVFXSO : ScriptableObject
{
    [SerializeField] private ESkillId _skillId;
    public ESkillId SkillId => _skillId;

    [SerializeField] private VFXTrack[] _tracks;
    public IReadOnlyCollection<VFXTrack> Tracks => _tracks;
}