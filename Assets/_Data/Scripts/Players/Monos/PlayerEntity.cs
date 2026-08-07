using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerEntity : CharacterEntity, IStartInit
{
    [Header("Installers")]
    [SerializeReference] private List<BaseInstaller> installerConfigs = new List<BaseInstaller>();
    [SerializeField] private SceneReceiverData _sceneReceiverData;
    [SerializeField] private SceneLoadManager _sceneLoadManager;

    private IPlayerTeamIntallerProvider _iPlayerTeamIntallerProvider;
    private IInventoryModelProvider _inventoryModelProvider;

    public override void Initialize()
    {
        base.Initialize();
        foreach(BaseInstaller installer in installerConfigs)
        {
            installer.Initialize();
            if(installer is IPlayerTeamIntallerProvider iPlayerTeamIntallerProvider)
            {
                _iPlayerTeamIntallerProvider = iPlayerTeamIntallerProvider;
            }
            if (installer is IInventoryModelProvider inventoryModelProvider)
            {
                _inventoryModelProvider = inventoryModelProvider;
            }
        }

        _sceneReceiverData.OnSceneLoaded
            .Subscribe(val => OnGetNewData(val))
            .AddTo(this);
    }


    [ContextMenu("Add PlayerMovementInstaller")]
    public void AddPlayerMovementInstaller() => installerConfigs.Add(new PlayerMovementInstaller());

    [ContextMenu("Add PlayerTeamInstaller")]
    public void AddPlayerTeamInstaller() => installerConfigs.Add(new PlayerTeamInstaller());

    [ContextMenu("Add InventoryInstaller")]
    public void AddInventoryInstaller() => installerConfigs.Add(new InventoryInstaller());

    private void OnGetNewData(SceneLoadModel sceneLoadModel)
    {
        _iPlayerTeamIntallerProvider.PlayerTeamController.UpdateTeamModel(sceneLoadModel.PlayerTeamModel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out MonsterEntity monsterEntity)) return;
        if (!_iPlayerTeamIntallerProvider.CanBattle) return;

        
        _sceneLoadManager.StartLoadScene("BattleScene", new SceneLoadModel
        {
            BatlleModel = new BattleModel
            {
                OpponentMonsterModel = monsterEntity.IMonsterModelProvider.CloneCurrentMonsterModel(),
                PlayerTeamModel = _iPlayerTeamIntallerProvider.ClonePlayerTeamModel(),
                BattleInventoryModel = new BattleInventoryModel
                {
                    RestoreInventory = new RestoreInventoryModel
                    {
                        Items = _inventoryModelProvider.CloneInventoryModel(_inventoryModelProvider.InventoryModel.RestoreInventory.Items),
                    },
                    CaptureInventory = new CaptureInventoryModel
                    {
                        Items = _inventoryModelProvider.CloneInventoryModel(_inventoryModelProvider.InventoryModel.CaptureInventory.Items),
                    }
                }
            },
        });
    }
}
