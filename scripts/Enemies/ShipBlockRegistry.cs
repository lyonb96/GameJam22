using Godot;

public static class ShipBlockRegistry
{
    public static PackedScene ArmorBlock { get; private set; }

    public static PackedScene ThrusterBlock { get; private set; }

    public static void EnsureLoaded()
    {
        if (ArmorBlock is null)
        {
            ArmorBlock = ResourceLoader.Load("res://scenes/ArmorBlock.tscn") as PackedScene;
        }
        if (ThrusterBlock is null)
        {
            ThrusterBlock = ResourceLoader.Load("res://scenes/ThrusterBlock.tscn") as PackedScene;
        }
    }
}