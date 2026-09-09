using Firebase.Firestore;
using System;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public partial class PlayerDataController : IPlayerData, IInitializable, IDisposable
{
    private FirebaseFirestore _fireStore;
    private readonly IPlayer _player;
    private readonly IPlayerTeamProvider _playerTeamProvider;
    private readonly IPlayerInventoryProvider _playerInventoryProvider;
    private readonly IFirebaseInitializer _firebaseInitializer;
    private readonly IBattleManager _battleManager;
    private readonly CompositeDisposable _disposable = new();
    private string _playerId;

    public PlayerDataController(
        IPlayer player,
        IFirebaseInitializer firebaseInitializer, 
        IPlayerTeamProvider playerTeamProvider, 
        IPlayerInventoryProvider playerInventoryProvider,
        IBattleManager battleManager)
    {
        _player = player;
        _firebaseInitializer = firebaseInitializer;
        _playerTeamProvider = playerTeamProvider;
        _playerInventoryProvider = playerInventoryProvider;
        _battleManager = battleManager;
    }

    public void Initialize()
    {
        if (_firebaseInitializer.IsInitialized)
        {
            HandleInitFirebaseCompleted();
            return;
        }

        _firebaseInitializer.OnFirebaseInitialized
            .Take(1)
            .Subscribe(_ => HandleInitFirebaseCompleted())
            .AddTo(_disposable);
    }

    private void HandleInitFirebaseCompleted()
    {
        Debug.Log("PlayerDataController Initialized");
        _playerId = SystemInfo.deviceUniqueIdentifier;
        _fireStore = FirebaseFirestore.DefaultInstance;
        _playerTeamProvider.SetPlayerDataController(this);
        _playerInventoryProvider.SetPlayerData(this);

        LoadData();
    }

    private async void SaveData(PlayerSaveDataModel playerSaveDataModel)
    {
        DocumentReference doc = _fireStore.Collection("Players").Document(_playerId);
        await doc.SetAsync(playerSaveDataModel);
        Debug.Log("PlayerDataController: Save data completed");
    }

    private async void LoadData()
    {
        Debug.Log("PlayerDataController: LoadData");
        DocumentReference documentReference = _fireStore.Collection("Players").Document(_playerId);
        DocumentSnapshot snapshot = await documentReference.GetSnapshotAsync();

        if(snapshot.Exists)
        {
            Debug.Log("PlayerDataController: Data available");
            HandleLoadPlayerData(snapshot.ConvertTo<PlayerSaveDataModel>());
        }
        else
        {
            Debug.Log("PlayerDataController: Data not available");
            PlayerDataConstructor();
        }
    }

    private void HandleLoadPlayerData(PlayerSaveDataModel playerSaveDataModel)
    {
        Debug.Log("PlayerDataController: HandleLoadPlayerData");
        _onLoadData.OnNext(playerSaveDataModel);
    }

    private void PlayerDataConstructor()
    {
        Debug.Log("PlayerDataController: PlayerDataConstructor");
        _playerTeamProvider.PlayerTeamConstructor();
        _playerInventoryProvider.InventoryConstructor();
        PlayerSaveDataModel data = new PlayerSaveDataModel
        {
            PlayerDataModel = PlayerDataFactory.Create(_player.Position, _battleManager.MapManager.MapType),
            InventoryDataModel = InventoryDataFactory.Create(_playerInventoryProvider),
            MonsterTeamDataModel = MonsterDataFactory.Create(_playerTeamProvider),
        };

        SaveData(data);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}