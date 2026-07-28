using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite _Image;
    public Sprite Image => _Image;

    [SerializeField] private int _ID;
    public int ID => _ID;

    [SerializeField] private string _Name;
    public string Name => _Name;

    [SerializeField] private EItemEffect _effects;
    public EItemEffect Effects => _effects;

    [SerializeField] private float _value;
    public float Value => _value;

    [SerializeField] private int _buyPrice;
    public int BuyPrice => _buyPrice;

    [SerializeField] private int _sellPrice;
    public int SellPirce => _sellPrice;           

    [TextArea(3, 5)]
    [SerializeField] private string _description;
    public string Description => _description;
}
