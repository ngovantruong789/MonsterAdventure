using UnityEngine;

public class CharacterAnimatorController : LifetimeScope, IStartInit
{
    [SerializeField] protected Animator _animator;

    public virtual void Initialize()
    {
        _animator = _animator == null ? GetComponent<Animator>() : _animator;
    }
}