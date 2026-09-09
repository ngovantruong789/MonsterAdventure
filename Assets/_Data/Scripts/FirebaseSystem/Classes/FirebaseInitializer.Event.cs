using System;
using UniRx;
using UnityEngine;

public partial class FirebaseInitializer
{
    private readonly Subject<Unit> _onFirebaseInitialized = new();
    public IObservable<Unit> OnFirebaseInitialized => _onFirebaseInitialized;
}