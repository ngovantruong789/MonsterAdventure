public partial class SceneLoadController : ISceneLoadController
{
    public void ToggleLoadScene(bool isOpen)
    {
        _onLoadScene.OnNext(isOpen);
    }

    public void ToggleLoadSceneCompleted(bool isOpen)
    {
        _onToggleSceneCompleted.OnNext(isOpen);
    }
}
