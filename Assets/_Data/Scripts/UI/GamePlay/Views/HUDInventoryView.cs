using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private Button _btnCloseMenu;
    [SerializeField] private Button _btnCaptureTab;
    [SerializeField] private Button _btnRestoreTab;
    [SerializeField] private RectTransform _btnItemPrefab;
    [SerializeField] private List<BtnItemInfor> _btnItemRestoreInfors = new();
    [SerializeField] private List<BtnItemInfor> _btnItemCaptureInfors = new();
    [SerializeField] private RectTransform _inventoryCaptureParent;
    [SerializeField] private RectTransform _inventoryRestoreParent;

    private HUDInventoryViewData _hUDInventoryViewData;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _btnCloseMenu.onClick.AddListener(() =>
        {
            transform.gameObject.SetActive(false);
            ResetValue();
        });
        _btnCaptureTab.onClick.AddListener(() =>
        {
            _inventoryRestoreParent.gameObject.SetActive(false);
            _inventoryCaptureParent.gameObject.SetActive(true);
        });
        _btnRestoreTab.onClick.AddListener(() =>
        {
            _inventoryCaptureParent.gameObject.SetActive(false);
            _inventoryRestoreParent.gameObject.SetActive(true);
        });
    }

    public void SetData(HUDInventoryViewData hUDInventoryViewData)
    {
        _hUDInventoryViewData = hUDInventoryViewData;
    }

    public void UpdateInventoryView()
    {
        HandleUpdateInventory(_hUDInventoryViewData.CaptureInventory.Items, _btnItemCaptureInfors, _inventoryCaptureParent);
        HandleUpdateInventory(_hUDInventoryViewData.RestoreInventory.Items, _btnItemRestoreInfors, _inventoryRestoreParent);
    }

    private void HandleUpdateInventory(List<ItemViewData> items, List<BtnItemInfor> itemInfors, RectTransform parent)
    {
        foreach(BtnItemInfor itemInfor in itemInfors)
        {
            Destroy(itemInfor.gameObject);
        }
        itemInfors.Clear();

        foreach (ItemViewData item in items)
        {
            SpawnItem(item, itemInfors, parent);
        }
    }

    private void SpawnItem(ItemViewData itemViewData,List<BtnItemInfor> itemInfors, RectTransform parent)
    {
        BtnItemInfor btnItemInfor = Instantiate(_btnItemPrefab, parent).GetComponent<BtnItemInfor>();
        btnItemInfor.IdItem = itemViewData.Id;
        btnItemInfor.ItemNameText.text = itemViewData.Name;
        btnItemInfor.QuantityText.text = itemViewData.Quantity.ToString();
        btnItemInfor.ItemType = itemViewData.ItemType;
        btnItemInfor.ImgIcon.sprite = itemViewData.Image;
        btnItemInfor.DescriptionText.text = itemViewData.Description;
        btnItemInfor.gameObject.SetActive(true);
        itemInfors.Add(btnItemInfor);
    }

    private void ResetValue()
    {
        _inventoryCaptureParent.gameObject.SetActive(false);
        _inventoryRestoreParent.gameObject.SetActive(true);
    }
}