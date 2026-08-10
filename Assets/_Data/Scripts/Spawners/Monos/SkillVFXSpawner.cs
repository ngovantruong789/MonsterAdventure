using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillVFXSpawner : BaseMonoBehaviour, ISpawner, IStartInit
{
    [SerializeField] private Transform obj;
    [SerializeField] private Transform holder;

    private List<ISkillVFXEntity> skillVfxs = new();

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        skillVfxs = obj.GetComponentsInChildren<ISkillVFXEntity>().ToList();
        skillVfxs.ForEach(x => x.CurrentTransform.gameObject.SetActive(false));
    }

    public Transform Spawn(ESkillId eSkillId, Vector3 position, bool isActive = false)
    {
        foreach(ISkillVFXEntity skillVFXEntity in skillVfxs)
        {
            if(skillVFXEntity.ESkillId == eSkillId)
            {
                Transform vfx = Instantiate(skillVFXEntity.CurrentTransform, position, Quaternion.identity);
                vfx.gameObject.SetActive(isActive);
                vfx.parent = holder;
                return vfx;
            }
        }

        return null;
    }
}