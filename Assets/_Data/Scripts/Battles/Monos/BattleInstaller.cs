using System;
using UnityEngine;

[Serializable]
public class BattleInstaller : BaseInstaller, IBattleProvider
{
    [SerializeField] private BattleMonsterView _battleMonsterView;

    private BattleMonsterPresenter _battleMonsterPresenter;
    private BattleModel _battleModel;
    public BattleModel BattleModel { get => _battleModel; set => _battleModel = value; }

    public override void Initialize()
    {
        base.Initialize();
        if(BattleModel != null)
        {
            _battleMonsterPresenter = new BattleMonsterPresenter(_battleMonsterView, BattleModel);
        }
    }
}
