using TMPro;
using UnityEngine;

public class BattleButtonSkillInfor : LifetimeScope
{
    [SerializeField] private TextMeshProUGUI _skillNameText;
    public TextMeshProUGUI SkillNameText { get => _skillNameText; set => _skillNameText = value; }

    [SerializeField] private ESkillType _skillType;
    public ESkillType SkillType { get => _skillType; set => _skillType = value; }
}
