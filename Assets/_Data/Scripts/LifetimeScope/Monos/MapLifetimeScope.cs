
using VContainer;
using UnityEngine;
using System.Collections.Generic;

public class MapLifetimeScope : GameLifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        //Bush
        BushEntity[] bushes = FindObjectsByType<BushEntity>(UnityEngine.FindObjectsSortMode.None);
        foreach (var bush in bushes)
        {
            builder.RegisterBuildCallback(container =>
            {
                container.Inject(bush);
            });
        }
    }
}