using System;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

[Serializable]
public class BattleInstaller : BaseInstaller, IBattleProvider
{
    [SerializeField] private BattleMonsterWorldSpaceView _battleMonsterView;
    [SerializeField] private HUDBattleMonsterView _hUDBattleMonsterView;
    [SerializeField] private BattleManager _battleManager;

    private BattleTurnController _battleTurnController;
    private BattleMonsterController _battleMonsterController;
    private BattleMonsterPresenter _battleMonsterPresenter;
    private BattleModel _battleModel;
    public BattleModel BattleModel { get => _battleModel; set => _battleModel = value; }

    public override void Initialize()
    {
        base.Initialize();
        if(BattleModel != null)
        {
            _battleMonsterController = new BattleMonsterController(_battleModel, new DamageCalculator());
            _battleMonsterPresenter = new BattleMonsterPresenter(_battleMonsterView, 
                _hUDBattleMonsterView, 
                BattleModel, 
                _battleManager,
                _battleMonsterController);
            _battleTurnController = new BattleTurnController(_battleMonsterController);
        }
    }
}
