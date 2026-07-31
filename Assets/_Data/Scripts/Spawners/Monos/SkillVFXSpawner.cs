using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillVFXSpawner : LifetimeScope, ISpawner, IStartInit
{
    [SerializeField] private List<SkillVFXEntity> skillVfxs = new();
    [SerializeField] private Transform obj;
    [SerializeField] private Transform holder;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        skillVfxs = obj.GetComponentsInChildren<SkillVFXEntity>().ToList();
        skillVfxs.ForEach(x => x.gameObject.SetActive(false));
    }

    public Transform Spawn(ESkillId eSkillId, Vector3 position, bool isActive)
    {
        foreach(SkillVFXEntity skillVFXEntity in skillVfxs)
        {
            if(skillVFXEntity.ESkillId == eSkillId)
            {
                Transform vfx = Instantiate(skillVFXEntity.transform, position, Quaternion.identity);
                vfx.gameObject.SetActive(isActive);
                vfx.parent = holder;
                return vfx;
            }
        }

        return null;
    }
}