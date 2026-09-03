using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHudButtonInfor : BaseMonoBehaviour
{
    [SerializeField] private Button _button;
    public Button Button => _button;

    [SerializeField] private TextMeshProUGUI _btnText;
    public TextMeshProUGUI BtnText => _btnText;
}