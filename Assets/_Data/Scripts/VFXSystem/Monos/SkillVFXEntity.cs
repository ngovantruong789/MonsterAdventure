using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillVFXEntity : BaseMonoBehaviour, ISkillVFXEntity
{
    [SerializeField] private ESkillId eSkillId;
    public ESkillId ESkillId => eSkillId;

    [SerializeField] private List<VFXTrack> vfxTracks;
    [SerializeField] private float _timeDisable = 1f;
    [SerializeField] private float _timeDelayCompleted = 1f;

    public Action PlayVFXCompleted { get; set; }
    public Transform CurrentTransform => transform;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(PlaySkillVFXCoroutine());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ResetValue();
    }

    private IEnumerator PlaySkillVFXCoroutine()
    {
        foreach (VFXTrack track in vfxTracks)
        {
            if (track.timeActive > 0f)
            {
                yield return new WaitForSeconds(track.timeActive);
            }
            ActiveVFX(track);
        }

        yield return new WaitForSeconds(_timeDelayCompleted);
        PlayVFXCompleted?.Invoke();

        yield return new WaitForSeconds(_timeDisable);
        gameObject.SetActive(false);
    }

    private void ActiveVFX(VFXTrack track)
    {
        track.VFXObj.SetActive(true);
    }

    private void ResetValue()
    {
        foreach (VFXTrack track in vfxTracks)
        {
            track.VFXObj.SetActive(false);
        }
    }
}