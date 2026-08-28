using System;
using UniRx;
using VContainer.Unity;

public class PlayerInventoryPresenter : IStartable, IDisposable
{
    private readonly HUDInventoryView _hUDInventoryView;
    private readonly IInventoryProvider _inventoryProvider;
    private readonly IBattleManager _battleManager;
    private readonly CompositeDisposable _disposable = new();

    public PlayerInventoryPresenter(HUDInventoryView hUDInventoryView, IInventoryProvider inventoryProvider, IBattleManager battleManager)
    {
        _battleManager = battleManager;
        _inventoryProvider = inventoryProvider;
        _hUDInventoryView = hUDInventoryView;
    }

    public void Start()
    {
        UpdateView();
        _battleManager.OnBattleStatus
            .Subscribe(val =>
            {
                if (!val)
                {
                    UpdateView();
                }
            }).AddTo(_disposable);
    }

    private void UpdateView()
    {
        HUDInventoryViewData hUDInventoryViewData = new();
        hUDInventoryViewData.RestoreInventory.Items = ItemModelFactory.ConvertListItemViewModelToItemViewData(_inventoryProvider.RestoreInventoryModel.Items);
        hUDInventoryViewData.CaptureInventory.Items = ItemModelFactory.ConvertListItemViewModelToItemViewData(_inventoryProvider.CaptureInventoryModel.Items);
        _hUDInventoryView.SetData(hUDInventoryViewData);
        _hUDInventoryView.UpdateInventoryView();
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}
