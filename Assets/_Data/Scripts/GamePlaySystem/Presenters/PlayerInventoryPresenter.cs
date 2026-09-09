using System;
using UniRx;
using VContainer.Unity;

public class PlayerInventoryPresenter : IStartable, IDisposable
{
    private readonly HUDInventoryView _hUDInventoryView;
    private readonly IPlayerInventoryProvider _playerInventoryProvider;
    private readonly IBattleManager _battleManager;
    private readonly CompositeDisposable _disposable = new();

    public PlayerInventoryPresenter(HUDInventoryView hUDInventoryView, IPlayerInventoryProvider playerInventoryProvider, IBattleManager battleManager)
    {
        _battleManager = battleManager;
        _playerInventoryProvider = playerInventoryProvider;
        _hUDInventoryView = hUDInventoryView;
    }

    public void Start()
    {
        _playerInventoryProvider.OnPlayerInventoryChanged
            .Subscribe(_ => UpdateView())
            .AddTo(_disposable);

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
        hUDInventoryViewData.RestoreInventory.Items = ItemModelFactory.ConvertListItemViewModelToItemViewData(_playerInventoryProvider.RestoreInventoryModel.Items);
        hUDInventoryViewData.CaptureInventory.Items = ItemModelFactory.ConvertListItemViewModelToItemViewData(_playerInventoryProvider.CaptureInventoryModel.Items);
        _hUDInventoryView.SetData(hUDInventoryViewData);
        _hUDInventoryView.UpdateInventoryView();
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}
