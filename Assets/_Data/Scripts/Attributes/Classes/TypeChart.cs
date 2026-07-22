using System.Collections.Generic;

public static class TypeChart
{
    private static readonly Dictionary<(EElementType, EElementType), float> _chart = new()
        {
            { (EElementType.Fire, EElementType.Grass), 2f },
            { (EElementType.Fire, EElementType.Water), 0.5f },
            { (EElementType.Fire, EElementType.Fire), 0.5f },
            { (EElementType.Fire, EElementType.Ice), 2f },
            { (EElementType.Fire, EElementType.Electric), 1f },
            { (EElementType.Fire, EElementType.Ground), 1f },

            { (EElementType.Water, EElementType.Grass), 0.5f },
            { (EElementType.Water, EElementType.Water), 0.5f },
            { (EElementType.Water, EElementType.Fire), 2f },
            { (EElementType.Water, EElementType.Ice), 1f },
            { (EElementType.Water, EElementType.Electric), 1f },
            { (EElementType.Water, EElementType.Ground), 2f },

            { (EElementType.Grass, EElementType.Grass), 0.5f },
            { (EElementType.Grass, EElementType.Water), 2f },
            { (EElementType.Grass, EElementType.Fire), 0.5f },
            { (EElementType.Grass, EElementType.Ice), 1f },
            { (EElementType.Grass, EElementType.Electric), 1f },
            { (EElementType.Grass, EElementType.Ground), 2f },

            { (EElementType.Electric, EElementType.Grass), 0.5f },
            { (EElementType.Electric, EElementType.Water), 2f },
            { (EElementType.Electric, EElementType.Fire), 1f },
            { (EElementType.Electric, EElementType.Ice), 1f },
            { (EElementType.Electric, EElementType.Electric), 0.5f },
            { (EElementType.Electric, EElementType.Ground), 0f },

            { (EElementType.Ice, EElementType.Grass), 2f },
            { (EElementType.Ice, EElementType.Water), 0.5f },
            { (EElementType.Ice, EElementType.Fire), 0.5f },
            { (EElementType.Ice, EElementType.Ice), 0.5f },
            { (EElementType.Ice, EElementType.Electric), 1f },
            { (EElementType.Ice, EElementType.Ground), 2f },

            { (EElementType.Ground, EElementType.Grass), 0.5f },
            { (EElementType.Ground, EElementType.Water), 1f },
            { (EElementType.Ground, EElementType.Fire), 2f },
            { (EElementType.Ground, EElementType.Ice), 1f },
            { (EElementType.Ground, EElementType.Electric), 2f },
            { (EElementType.Ground, EElementType.Ground), 1f },
        };

    public static float GetMultiplier(EElementType attack, EElementType defend)
    {
        return _chart.TryGetValue((attack, defend), out float multiplier)
            ? multiplier
            : 1f;
    }
}