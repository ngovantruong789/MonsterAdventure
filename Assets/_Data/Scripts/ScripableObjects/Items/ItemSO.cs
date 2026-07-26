using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("1. Thông tin giao diện (UI)")]
    public string itemName;         

    [TextArea(3, 5)]
    public string description;      
    public Sprite itemImage;         

    [Header("2. Định danh & Kinh tế")]
    public int itemID;              
    public EItemType itemType;       
    public int buyPrice;            
    public int sellPrice;           

    [Header("3. Chỉ số Tác dụng (Effect Payload)")]
    public float effectValue;       
    public bool isConsumable;       
}
