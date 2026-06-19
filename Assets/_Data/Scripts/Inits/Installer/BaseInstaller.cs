using System;

[Serializable]
public abstract class BaseInstaller : IStartInit
{
    public string installerName;

    public virtual void Initialize()
    {
        
    }
}