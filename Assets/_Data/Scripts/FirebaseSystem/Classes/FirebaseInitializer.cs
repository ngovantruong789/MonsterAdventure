using Firebase;
using Firebase.Extensions;
using UnityEngine;
using VContainer.Unity;

public partial class FirebaseInitializer : IInitializable, IFirebaseInitializer
{
    private FirebaseApp _firebaseApp;
    private bool _isInitialized;
    public bool IsInitialized => _isInitialized;

    public void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync()
            .ContinueWithOnMainThread(task =>
            {
                DependencyStatus status = task.Result;
                if (status == DependencyStatus.Available)
                {
                    _firebaseApp = FirebaseApp.DefaultInstance;
                    _isInitialized = true;
                    _onFirebaseInitialized.OnNext(default);
                    Debug.Log("Firebase connected!!!");
                }
                else
                {
                    Debug.LogError("Firebase init failed: " + status);
                }
            });
    }
}