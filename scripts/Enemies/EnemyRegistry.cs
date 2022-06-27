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
            ["Crusher"] = new EnemySchema
            {
                Name = "Crusher",
                Parts = new List<EnemyPart>
                {
                    // Top layer armor
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
                    // Mid layer armor
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
                    // Back layer armor
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, 3),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, 4),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, -2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, -3),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, -4),
                    },
                    // Tail fins
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-2, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-2, -1),
                    },
                    // Thrusters
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-1, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-2, 3),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-2, -3),
                    },
                    // Shields
                    new EnemyPart
                    {
                        Type = "Shield",
                        Position = new Vector2(-1, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Shield",
                        Position = new Vector2(-1, -1),
                    },
                    // Turrets
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(0, 4),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(0, -4),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(2, 1),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(2, -1),
                    },
                }
            },
            ["Ender"] = new EnemySchema
            {
                Name = "Ender",
                Parts = new List<EnemyPart>
                {
                    // Top layer armor
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
                    // Mid layer armor
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
                    // Next layer back
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-1, -2),
                    },
                    // Next layer
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-2, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-2, -2),
                    },
                    // Next layer
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-3, -2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-3, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-3, 2),
                    },
                    // Second to last armor layer
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-4, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-4, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-4, 3),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-4, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-4, -2),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-4, -3),
                    },
                    // Final armor layer
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-5, 3),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-5, 4),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-5, -3),
                    },
                    new EnemyPart
                    {
                        Type = "Armor",
                        Position = new Vector2(-5, -4),
                    },
                    // Shields
                    new EnemyPart
                    {
                        Type = "Shield",
                        Position = new Vector2(-1, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Shield",
                        Position = new Vector2(-2, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Shield",
                        Position = new Vector2(-2, 1),
                    },
                    // Lasers
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(2, 1),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(2, -1),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(0, 2),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(0, -2),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(-4, 4),
                    },
                    new EnemyPart
                    {
                        Type = "LaserCannon",
                        Position = new Vector2(-4, -4),
                    },
                    // Thrusters
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-2, 0),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-3, 1),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-3, -1),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-5, 2),
                    },
                    new EnemyPart
                    {
                        Type = "Thruster",
                        Position = new Vector2(-5, -2),
                    },
                }
            }
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