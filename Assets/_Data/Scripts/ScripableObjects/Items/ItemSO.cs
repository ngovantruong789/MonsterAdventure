using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("1. Thông tin giao diện (UI)")]
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    [TextArea(3, 5)]
    [SerializeField] private string _description;
    public string Description => _description;
    [SerializeField] private Sprite _itemImage;
    public Sprite ItemImage => _itemImage;

    [Header("2. Định danh & Kinh tế")]
    [SerializeField] private int _itemID;
    public int ItemID => _itemID;
    /*
    public EItemType itemType;
    
    public int buyPrice;            
    public int sellPrice;*/           

    [Header("3. Chỉ số Tác dụng (Effect Payload)")]
    [SerializeField] private EItemEffect[] _itemEffects;
    public EItemEffect[] ItemEffects => _itemEffects;
    [SerializeField] private float _value;
    public float Value => _value;         
}
