using System;
using UnityEngine;

[Serializable]
public class StatConfig
{
    [SerializeField] private EStatType _statType;
    public EStatType StatType { get => _statType; set => _statType = value; }

    [SerializeField] private int[] _growthPerLevels;
    public int[] GrowthPerLevels => _growthPerLevels;
}

public static class StatCalculator
{
    public static int CalculateStatPerLevel(int[] growthPerLevels, int level)
    {
        if (level == 1) return growthPerLevels[0];//Level 1 thì lấy chỉ số đầu

        int sumBuffLevel = 0;//Tổng buff của các cấp độ hiện tại
        int count = Mathf.Min(level, growthPerLevels.Length);   
        for (int i = 0; i < count; i++)
        {
            sumBuffLevel += growthPerLevels[i];
        }

        int residualLevel = level - growthPerLevels.Length;
        if (residualLevel > 0)//Nếu cấp độ hiện tại lớn hơn cấp độ phát triển trong list thì cộng dựa vào chỉ số cuối
        {
            int lastValue = growthPerLevels[growthPerLevels.Length - 1];
            for (int i = 0; i < residualLevel; i++)
            {
                sumBuffLevel += lastValue;
            }
        }

        return sumBuffLevel;
    }
}