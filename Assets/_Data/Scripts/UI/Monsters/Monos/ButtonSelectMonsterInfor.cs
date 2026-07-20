using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectMonsterInfor : LifetimeScope
{
    [SerializeField] private Button _button;
    public Button Button { get => _button; set => _button = value; }

    [SerializeField] private Animator _monsterAnimator;
    public Animator MonsterAnimator { get => _monsterAnimator; set => _monsterAnimator = value; }

    [SerializeField] private TextMeshProUGUI _monsterNameText;
    public TextMeshProUGUI MonsterNameText { get => _monsterNameText; set => _monsterNameText = value; }

    [SerializeField] private TextMeshProUGUI _healthText;
    public TextMeshProUGUI HealthText { get => _healthText; set => _healthText = value; }

    [SerializeField] private TextMeshProUGUI _levelText;
    public TextMeshProUGUI LevelText { get => _levelText; set => _levelText = value; }

    [SerializeField] private Slider _healthBar;
    public Slider HealthBar { get => _healthBar; set => _healthBar = value; }

    [SerializeField] private Image _imgSelected;
    public Image ImgSelected { get => _imgSelected; set => _imgSelected = value; }

    [SerializeField] private Image _imgCantSelect;
    public Image ImgCantSelect { get => _imgCantSelect; set => _imgCantSelect = value; }

    [SerializeField] private int _monsterIndex;
    public int MonsterIndex { get => _monsterIndex; set => _monsterIndex = value; }

    [SerializeField] private bool _canSelect;
    public bool CanSelect { get => _canSelect; set => _canSelect = value; }
}
