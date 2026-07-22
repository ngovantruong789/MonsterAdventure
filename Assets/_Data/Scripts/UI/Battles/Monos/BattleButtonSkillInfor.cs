using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleButtonSkillInfor : LifetimeScope
{
    [SerializeField] private Button _btnSkill;
    public Button BtnSkill { get => _btnSkill; set => _btnSkill = value; }

    [SerializeField] private TextMeshProUGUI _skillNameText;
    public TextMeshProUGUI SkillNameText { get => _skillNameText; set => _skillNameText = value; }

    [SerializeField] private Image _imgSelected;
    public Image ImgSelected { get => _imgSelected; set => _imgSelected = value; }

    [SerializeField] private ESkillId _skillId;
    public ESkillId ESkillId { get => _skillId; set => _skillId = value; }

    [SerializeField] private ESkillType _skillType;
    public ESkillType SkillType { get => _skillType; set => _skillType = value; }

    [SerializeField] private int _skillIndex;
    public int SkillIndex { get => _skillIndex; set => _skillIndex = value; }
}
