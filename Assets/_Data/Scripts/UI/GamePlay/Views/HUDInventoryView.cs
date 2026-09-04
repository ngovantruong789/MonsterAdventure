using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI _iconNameTextDetail;
    [SerializeField] private Image _imgIconDetail;
    [SerializeField] private TextMeshProUGUI _descriptionTextDetail;
    private BtnItemInfor _itemCurrentSelected;

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

        _btnItemRestoreInfors.ForEach(item => item.BtnItem.onClick.AddListener(() => SelectItem(item)));
        _btnItemCaptureInfors.ForEach(item => item.BtnItem.onClick.AddListener(() => SelectItem(item)));
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

    private void SelectItem(BtnItemInfor itemSelected)
    {
        _iconNameTextDetail.text = itemSelected.ItemNameText.text;
        _imgIconDetail.sprite = itemSelected.ImgIcon.sprite;
        _descriptionTextDetail.text = itemSelected.DescriptionText.text;

        if (_itemCurrentSelected != null)
        {
            _itemCurrentSelected.ImgSelectedItem.gameObject.SetActive(false);
        }

        _itemCurrentSelected = itemSelected;
        _itemCurrentSelected.ImgSelectedItem.gameObject.SetActive(true);
    }

    private void ResetValue()
    {
        _inventoryCaptureParent.gameObject.SetActive(false);
        _inventoryRestoreParent.gameObject.SetActive(true);
        _iconNameTextDetail.text = null;
        _imgIconDetail.sprite = null;
        _descriptionTextDetail.text = null;
        if (_itemCurrentSelected != null)
        {
            _itemCurrentSelected.ImgSelectedItem.gameObject.SetActive(false);
            _itemCurrentSelected = null;
        }
    }
}