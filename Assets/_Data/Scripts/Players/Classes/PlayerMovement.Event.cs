using System;
using UniRx;

public partial class PlayerMovement
{
    private ReactiveProperty<bool> _isMoveable = new();
    public IObservable<bool> IsMoveable => _isMoveable;
}