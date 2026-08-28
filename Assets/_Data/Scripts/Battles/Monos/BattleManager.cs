using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public partial class BattleManager : GameLifetimeScope, IStartable, IBattleManager
{
    [Inject] private SceneLoadManager _sceneLoadManager;
    [Inject] private BattleModel _battleModel;

    private IPlayer _player;
    private IMapManager _mapManager;

    public void Start()
    {
        _player = FindAnyObjectByType<PlayerEntity>();

        _sceneLoadManager.OnLoadScene
            .Subscribe(val =>
            {
                if (_player == null) return;
                _player.PlayerMovement.SetMove(val);
            })
            .AddTo(this);

        DontDestroyOnLoad(gameObject);
    }

    private MonsterModel GetMonsterModel()
    {
        int rand = Random.Range(1, 101);
        MonsterMapModel monsterMapModel = new MonsterMapModel();

        foreach(MonsterMapModel model in _mapManager.MapModel.MonsterMaps)
        {
            if(rand >= model.SpawnRate.x && rand <= model.SpawnRate.y)
            {
                monsterMapModel = model;
                break;
            }
        }
        if(monsterMapModel.MonsterSO == null) return null;

        int level = Random.Range((int)monsterMapModel.LevelOriginRange.x, (int)monsterMapModel.LevelOriginRange.y + 1);
        return MonsterModelFactory.Create(monsterMapModel.MonsterSO, level); ;
    }

    public void EnterBattle()
    {
        if (_onBattleStatus.Value) return;
        if(_player == null) return;
        if (!_player.CanBattle) return;

        _battleModel.OpponentMonsterModel = GetMonsterModel();
        if (_battleModel.OpponentMonsterModel == null) return;

        _sceneLoadManager.StartLoadScene("BattleScene");
        _onBattleStatus.Value = true;
    }

    public void EndBattle()
    {
        _sceneLoadManager.CloseSceneAttitive("BattleScene", "GamePlay");
        _onBattleStatus.Value = false;
    }

    public void SetMapManager(IMapManager mapManager)
    {
        _mapManager = mapManager;
    }
}