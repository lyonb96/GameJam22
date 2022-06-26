using System.Collections.Generic;
using Godot;

public static class EnemyRegistry
{
    public static Dictionary<string, EnemySchema> Enemies { get; set; }

    public static void Initialize()
    {
        if (Enemies != null)
        {
            return;
        }
        Enemies = new Dictionary<string, EnemySchema>()
        {
            ["Moth"] = new EnemySchema
            {
                Name = "Moth",
                ChallengeRating = 10,
                Parts = new List<EnemyPart>
                {
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(1, 0),
                    },
                }
            },
            ["Tussler"] = new EnemySchema
            {
                Name = "Tussler",
                ChallengeRating = 28,
                Parts = new List<EnemyPart>
                {
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(0, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(0, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(1, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-1, 0),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(1, -1),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(1, 1),
                    },
                }
            },
            ["Mangler"] = new EnemySchema
            {
                Name = "Mangler",
                ChallengeRating = 25,
                Parts = new List<EnemyPart>
                {
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(1, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(1, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(1, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(0, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(0, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(0, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(0, -2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-1, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-1, -2),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-2, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-2, -1),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(1, 2),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(1, -2),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(2, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Shield",
                        Position = new Vector2(-1, 0),
                    },
                }
            },
        };
        foreach (var enemy in Enemies)
        {
            var schema = enemy.Value;
            var cr = 0;
            foreach (var part in schema.Parts)
            {
                if (part.Type == "Armor")
                {
                    cr += 1;
                }
                else if (part.Type == "Thruster")
                {
                    cr += 5;
                }
                else if (part.Type == "LaserCannon")
                {
                    cr += 10;
                }
                else if (part.Type == "Shield")
                {
                    cr += 4;
                }
            }
            schema.ChallengeRating = cr;
        }
    }
}