using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public partial class PlayerInventoryController : IPlayerInventoryProvider, IStartable, IDisposable
{
    private PlayerInventoryModel _playerInventoryModel;
    private IReadOnlyList<ItemSO> _items;
    private IPlayerData _playerDataController;
    private readonly ItemDatabaseSO _itemDatabaseSO;
    private readonly CompositeDisposable _disposable = new();
    public InventoryModel RestoreInventoryModel => _playerInventoryModel.RestoreInventory;
    public InventoryModel CaptureInventoryModel => _playerInventoryModel.CaptureInventory;
    
    public PlayerInventoryController(PlayerInventoryModel inventoryModel, IReadOnlyList<ItemSO> items, ItemDatabaseSO itemDatabase)
    {
        _items = items;
        _playerInventoryModel = inventoryModel;
        _itemDatabaseSO = itemDatabase;
        Debug.Log("InventoryController Initialized");
    }

    public void Start()
    {
        
    }

    private void LoadItemData(InventoryDataModel inventoryDataModel)
    {
        PlayerInventoryModel playerInventoryModel = InventoryModelFactory.ConvertPlayerInventoryDataToPlayerInventoryModel(inventoryDataModel, _itemDatabaseSO);
        _playerInventoryModel = playerInventoryModel;
        RemoveItemWhenUsedUp();
        _onPlayerInventoryChanged.OnNext(default);
    }

    public void InventoryConstructor()
    {
        foreach (ItemSO item in _items)
        {
            AddItem(ItemModelFactory.Create(item, 1));
        }
    }

    public void AddItem(ItemModel item)
    {
        if (item.EffectItem == EItemEffect.RestoreHp ||
           item.EffectItem == EItemEffect.BurnHeal ||
           item.EffectItem == EItemEffect.FreezeHeal ||
           item.EffectItem == EItemEffect.BuffHealth ||
           item.EffectItem == EItemEffect.BuffDefense ||
           item.EffectItem == EItemEffect.BuffAttack ||
           item.EffectItem == EItemEffect.BuffSpeed ||
           item.EffectItem == EItemEffect.Awakening ||
           item.EffectItem == EItemEffect.ParalyzeHeal )
        {
            OnAddItem(_playerInventoryModel.RestoreInventory.Items, item);
        }
        else if (item.EffectItem == EItemEffect.Capture)
        {
            OnAddItem(_playerInventoryModel.CaptureInventory.Items, item);
        }
        else if (item.EffectItem == EItemEffect.Lucky)
        {
            OnAddItem(_playerInventoryModel.PlayerEquipment.Items, item);
        }
        /*else //Chưa có Item cho monster
        {
           OnAddItem(_inventoryModel.MonsterEquipment.Items, item);
        }*/

        _onPlayerInventoryChanged.OnNext(default);
    }

    private void OnAddItem(List<ItemModel> itemModels, ItemModel item)
    {
        bool isItemExist = false;

        foreach (ItemModel itemModel in itemModels)
        {
            if (item.Id == itemModel.Id)
            {
                itemModel.Quantity += item.Quantity;
                isItemExist = true;
                break;
            }
        }

        if (!isItemExist)
        {
            itemModels.Add(item);
        }
    }

    public void UpdateQuantityPlayerInventoryModel()
    {
        RemoveItemWhenUsedUp();
    }

    private void RemoveItemWhenUsedUp()
    {
        _playerInventoryModel.CaptureInventory.Items.RemoveAll(item => item.Quantity <= 0);
        _playerInventoryModel.RestoreInventory.Items.RemoveAll(item => item.Quantity <= 0);
        _playerInventoryModel.PlayerEquipment.Items.RemoveAll(item => item.Quantity <= 0);
        _playerInventoryModel.MonsterEquipment.Items.RemoveAll(item => item.Quantity <= 0);
    }

    public void SetPlayerData(IPlayerData playerData)
    {
        _playerDataController = playerData;
        _playerDataController.OnLoadData
            .Subscribe(val => LoadItemData(val.InventoryDataModel))
            .AddTo(_disposable);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}