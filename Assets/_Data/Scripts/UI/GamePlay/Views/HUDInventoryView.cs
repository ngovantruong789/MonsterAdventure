using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private Button _btnCloseMenu;

    private HUDInventoryViewData _hUDInventoryViewData;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _btnCloseMenu.onClick.AddListener(() => transform.gameObject.SetActive(false));
    }

    public void SetData(HUDInventoryViewData hUDInventoryViewData)
    {
        _hUDInventoryViewData = hUDInventoryViewData;
    }

    public void UpdateInventoryView()
    {
        HandleUpdateInventory(_hUDInventoryViewData.CaptureInventory.Items);
        HandleUpdateInventory(_hUDInventoryViewData.RestoreInventory.Items);
    }

    private void HandleUpdateInventory(List<ItemViewData> items)
    {

    }
}