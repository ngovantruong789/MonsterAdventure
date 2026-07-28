using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite _itemImage;
    public Sprite ItemImage => _itemImage;

    [SerializeField] private int _itemID;
    public int ItemID => _itemID;

    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    [SerializeField] private EItemEffect _itemEffects;
    public EItemEffect ItemEffects => _itemEffects;

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
