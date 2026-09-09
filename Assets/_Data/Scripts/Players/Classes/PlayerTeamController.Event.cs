using System;
using UniRx;

public partial class PlayerTeamController
{
    private readonly Subject<Unit> _onUpdatePlayerTeam = new();
    public IObservable<Unit> OnUpdatePlayerTeam => _onUpdatePlayerTeam;
}