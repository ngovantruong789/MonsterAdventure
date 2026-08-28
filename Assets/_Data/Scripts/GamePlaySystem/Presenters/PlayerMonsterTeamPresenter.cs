using System;
using UniRx;
using VContainer.Unity;

public class PlayerMonsterTeamPresenter : IStartable, IDisposable
{
    private readonly HUDMonsterTeamView _hUDMonsterTeamView;
    private readonly IPlayerTeamProvider _playerTeamProvider;
    private readonly IBattleManager _battleManager;
    private readonly CompositeDisposable _disposable = new();

    public PlayerMonsterTeamPresenter(HUDMonsterTeamView hUDMonsterTeamView, IBattleManager battleManager, IPlayerTeamProvider playerTeamProvider)
    {
        _hUDMonsterTeamView = hUDMonsterTeamView;
        _playerTeamProvider = playerTeamProvider;
        _battleManager = battleManager;
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
        UpdateTeamMonsterViewData();
        _hUDMonsterTeamView.UpdateMonsterTeam();
    }

    private void UpdateTeamMonsterViewData()
    {
        HUDMonsterTeamViewData hUDMonsterTeamViewData = new();
        hUDMonsterTeamViewData.MonsterTeams = MonsterModelFactory.ConvertListMonsterModelToMonsterViewData(_playerTeamProvider.TeamModel.PlayerTeam);
        _hUDMonsterTeamView.SetData(hUDMonsterTeamViewData);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}