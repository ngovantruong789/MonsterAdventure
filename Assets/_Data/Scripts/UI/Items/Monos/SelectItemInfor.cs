using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemInfor : BaseMonoBehaviour
{
    [SerializeField] private int _idItem;
    public int IdItem { get => _idItem; set => _idItem = value; }

    [SerializeField] private EItemType _itemType;
    public EItemType ItemType { get => _itemType; set => _itemType = value; }

    [SerializeField] private Button _btnItem;
    public Button BtnItem { get => _btnItem; set => _btnItem = value; }

    [SerializeField] private Image _imgSelectedItem;
    public Image ImgSelectedItem { get => _imgSelectedItem; set => _imgSelectedItem = value; }

    [SerializeField] private Image _imgIcon;
    public Image ImgIcon { get => _imgIcon; set => _imgIcon = value; }

    [SerializeField] private TextMeshProUGUI _itemNameText;
    public TextMeshProUGUI ItemNameText { get => _itemNameText; set => _itemNameText = value; }

    [SerializeField] private TextMeshProUGUI _descriptionText;
    public TextMeshProUGUI DescriptionText { get => _descriptionText; set => _descriptionText = value; }

    [SerializeField] private TextMeshProUGUI _quantityText;
    public TextMeshProUGUI QuantityText { get => _quantityText; set => _quantityText = value; }
}
