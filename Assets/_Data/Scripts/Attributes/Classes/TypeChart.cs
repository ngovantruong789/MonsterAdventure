using System.Collections.Generic;

public static class TypeChart
{
    private static readonly Dictionary<(EElementType, EElementType), float> _chart = new()
        {
            { (EElementType.Fire, EElementType.Grass), 2f },
            { (EElementType.Fire, EElementType.Water), 0.5f },
            { (EElementType.Fire, EElementType.Fire), 0.5f },
            { (EElementType.Fire, EElementType.Ice), 2f },
            { (EElementType.Fire, EElementType.Steel), 2f },

            { (EElementType.Water, EElementType.Grass), 0.5f },
            { (EElementType.Water, EElementType.Water), 0.5f },
            { (EElementType.Water, EElementType.Fire), 2f },
            { (EElementType.Water, EElementType.Ground), 2f },

            { (EElementType.Grass, EElementType.Grass), 0.5f },
            { (EElementType.Grass, EElementType.Water), 2f },
            { (EElementType.Grass, EElementType.Fire), 0.5f },
            { (EElementType.Grass, EElementType.Ground), 2f },
            { (EElementType.Grass, EElementType.Fly), 0.5f },

            { (EElementType.Electric, EElementType.Grass), 0.5f },
            { (EElementType.Electric, EElementType.Water), 2f },
            { (EElementType.Electric, EElementType.Electric), 0.5f },
            { (EElementType.Electric, EElementType.Ground), 0f },
            { (EElementType.Electric, EElementType.Fly), 2f },

            { (EElementType.Ice, EElementType.Grass), 2f },
            { (EElementType.Ice, EElementType.Water), 0.5f },
            { (EElementType.Ice, EElementType.Fire), 0.5f },
            { (EElementType.Ice, EElementType.Ice), 0.5f },
            { (EElementType.Ice, EElementType.Ground), 2f },
            { (EElementType.Ice, EElementType.Fly), 2f },
            { (EElementType.Ice, EElementType.Steel), 0.5f },

            { (EElementType.Ground, EElementType.Grass), 0.5f },
            { (EElementType.Ground, EElementType.Fire), 2f },
            { (EElementType.Ground, EElementType.Electric), 2f },
            { (EElementType.Ground, EElementType.Fly), 0f },
            { (EElementType.Ground, EElementType.Steel), 2f },

            { (EElementType.Fly, EElementType.Grass), 2f },
            { (EElementType.Fly, EElementType.Electric), 0.5f },
            { (EElementType.Fly, EElementType.Fighting), 2f },
            { (EElementType.Fly, EElementType.Steel), 0.5f },

            { (EElementType.Ghost, EElementType.Ghost), 2f },
            { (EElementType.Ghost, EElementType.Psychic), 2f },

            { (EElementType.Fighting, EElementType.Ice), 2f },
            { (EElementType.Fighting, EElementType.Fly), 0.5f },
            { (EElementType.Fighting, EElementType.Ghost), 0f },
            { (EElementType.Fighting, EElementType.Steel), 2f },
            { (EElementType.Fighting, EElementType.Psychic), 0.5f },
            { (EElementType.Fighting, EElementType.Fairy), 0.5f },

            { (EElementType.Steel, EElementType.Water), 0.5f },
            { (EElementType.Steel, EElementType.Fire), 0.5f },
            { (EElementType.Steel, EElementType.Ice), 2f },
            { (EElementType.Steel, EElementType.Electric), 0.5f },
            { (EElementType.Steel, EElementType.Steel), 0.5f },
            { (EElementType.Steel, EElementType.Fairy), 2f },

            { (EElementType.Psychic, EElementType.Fire), 0.5f },
            { (EElementType.Psychic, EElementType.Fighting), 2f },
            { (EElementType.Psychic, EElementType.Steel), 0.5f },

            { (EElementType.Fairy, EElementType.Fire), 0.5f },
            { (EElementType.Fairy, EElementType.Fighting), 2f },
            { (EElementType.Fairy, EElementType.Steel), 0.5f },

        };

    public static float GetMultiplier(EElementType attack, EElementType defend)
    {
        return _chart.TryGetValue((attack, defend), out float multiplier)
            ? multiplier
            : 1f;
    }
}