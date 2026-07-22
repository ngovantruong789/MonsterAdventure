using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "ScripableObjects/Skill")]
public class SkillSO : ScriptableObject
{
    [SerializeField] private ESkillId eSkillId;
    public ESkillId ESkillId => eSkillId;

    [SerializeField] private string _fullName;
    public string FullName => _fullName;

    [SerializeField] private ESkillType _skillType;
    public ESkillType SkillType => _skillType;

    [SerializeField] private EElementType _elementType;
    public EElementType ElementType => _elementType;

    [SerializeField] private int _damage;
    public int Damage => _damage;
}