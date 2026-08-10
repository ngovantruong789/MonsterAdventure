using System;
using UniRx;
using UnityEngine;

public partial class SceneLoadManager
{
    private Subject<Camera> _onCameraChanged = new();
    public IObservable<Camera> OnCameraChanged => _onCameraChanged;
}
