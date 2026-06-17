using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : LifetimeScope
{
    [SerializeField] private List<LifetimeScope> scripts;

    protected override void Start()
    {
        base.Start();
    }
}
