using System;
using UniRx;
using VContainer.Unity;

public class BattleLoadingHUDPresenter : IDisposable, IStartable
{
    private UIMapBattleView _uIMapBattleView;
    private readonly UIBattleMapModel _uIBattleMapModel;
    private readonly CompositeDisposable _disposable = new();

    public BattleLoadingHUDPresenter(BattleModel battleModel, UIMapBattleView uIMapBattleView)
    {
        _uIBattleMapModel = battleModel.UIBattleMapModel;
        _uIMapBattleView = uIMapBattleView;
    }

    public void Start()
    {
        _uIMapBattleView.SetData(MapFactory.ConvertUIBattleMapModelToUIMapBattleViewData(_uIBattleMapModel));
        _uIMapBattleView.UpdateUIBattleWithMap();
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}