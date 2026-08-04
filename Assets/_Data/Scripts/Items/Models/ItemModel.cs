using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public EItemEffect EffectItem { get; set; }
    public float Value { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}
