using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite _image;
    public Sprite Image => _image;

    [SerializeField] private int _id;
    public int Id => _id;

    [SerializeField] private string _name;
    public string Name => _name;

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
