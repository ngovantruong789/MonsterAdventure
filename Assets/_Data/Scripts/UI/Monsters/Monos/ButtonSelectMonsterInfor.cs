using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectMonsterInfor : LifetimeScope
{
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
}
