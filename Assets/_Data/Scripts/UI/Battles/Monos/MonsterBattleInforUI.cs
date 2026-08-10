using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBattleInforUI : BaseMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _monsterNameText;
    public TextMeshProUGUI MonsterNameText { get => _monsterNameText; set => _monsterNameText = value; }

    [SerializeField] private TextMeshProUGUI _healthValueText;
    public TextMeshProUGUI HealthValueText { get => _healthValueText; set => _healthValueText = value; }

    [SerializeField] private TextMeshProUGUI _levelText;
    public TextMeshProUGUI LevelText { get => _levelText; set => _levelText = value; }

    [SerializeField] private Slider _healthSlider;
    public Slider HealthSlider { get => _healthSlider; set => _healthSlider = value; }
}
