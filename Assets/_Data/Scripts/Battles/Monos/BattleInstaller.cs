using System;
using UnityEngine;

[Serializable]
public class BattleInstaller : BaseInstaller, IBattleProvider
{
    [SerializeField] private BattleMonsterWorldSpaceView _battleMonsterView;
    [SerializeField] private HUDBattleMonsterView _hUDBattleMonsterView;
    [SerializeField] private BattleManager _battleManager;

    private BattleMonsterPresenter _battleMonsterPresenter;
    private BattleModel _battleModel;
    public BattleModel BattleModel { get => _battleModel; set => _battleModel = value; }

    public override void Initialize()
    {
        base.Initialize();
        if(BattleModel != null)
        {
            _battleMonsterPresenter = new BattleMonsterPresenter(_battleMonsterView, _hUDBattleMonsterView, BattleModel, _battleManager);
        }
    }
}
