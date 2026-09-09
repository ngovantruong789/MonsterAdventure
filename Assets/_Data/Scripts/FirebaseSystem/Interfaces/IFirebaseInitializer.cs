using System;
using UniRx;

public interface IFirebaseInitializer
{
    IObservable<Unit> OnFirebaseInitialized { get; }
    bool IsInitialized { get; }
}