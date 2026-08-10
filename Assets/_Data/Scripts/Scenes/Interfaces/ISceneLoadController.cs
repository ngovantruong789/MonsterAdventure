using System;
using UniRx;

public interface ISceneLoadController
{
    IObservable<bool> OnToggleSceneCompleted { get; }
    IObservable<bool> OnLoadScene { get; }

    void ToggleLoadSceneCompleted(bool isOpen);
    void ToggleLoadScene(bool isOpen);
}
