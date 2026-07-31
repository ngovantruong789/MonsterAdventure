using System;
using UnityEngine;

public interface ISkillVFXEntity
{
    Action PlayVFXCompleted { get; set; }
    ESkillId ESkillId { get;}
    Transform CurrentTransform { get;}
}