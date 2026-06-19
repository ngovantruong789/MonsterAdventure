using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterEntity : LifetimeScope, IStartInit
{
    [SerializeField] private List<LifetimeScope> _characterScripts = new List<LifetimeScope>();

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public virtual void Initialize()
    {
        _characterScripts = GetComponentsInChildren<LifetimeScope>()
            .Where(lifeTime => lifeTime != this)
            .ToList();

        foreach(var script in _characterScripts)
        {
            if (script.TryGetComponent(out IStartInit init))
            {
                init.Initialize();
            }
        }
    }
}
