using System;
using UnityEngine;

[Serializable]
public class CaptureRateConfig
{
    [SerializeField] private float _baseCatchDifficulty;
    public float BaseCatchDifficulty => _baseCatchDifficulty;

    [SerializeField] private float _levelBonus;
    public float LevelBonus => _levelBonus;

    public float CalculatetDifficultPerLevel(int level)
    {
        return _baseCatchDifficulty + level * _levelBonus;
    }
}

public static class CaptureCalculator
{
    public static float CalculateDiffercultCapturePerLevel(CaptureRateConfig config, int level)
    {
        float diff = config.BaseCatchDifficulty;
        for (int i = 0; i < level; i++)
        {
            diff += config.LevelBonus;
        }

        return diff;
    }
}