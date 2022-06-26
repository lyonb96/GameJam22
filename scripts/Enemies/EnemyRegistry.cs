using System.Collections.Generic;
using Godot;

public static class EnemyRegistry
{
    public static Dictionary<string, EnemySchema> Enemies { get; set; }

    public static void Initialize()
    {
        Enemies = new Dictionary<string, EnemySchema>()
        {
            ["Basic"] = new EnemySchema
            {
                Name = "Basic Enemy",
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
        };
    }
}