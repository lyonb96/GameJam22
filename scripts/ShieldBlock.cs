using Godot;

public class ShieldBlock : ShipBlock
{
    public ShieldBlock()
    {
        AttachableSides = Utils.AllSides;
        StatBlockMods = stats =>
        {
            stats.MaxShield += 50.0F;
            if (stats.ShieldRegenRate <= 0)
            {
                stats.ShieldRegenRate = 10.0F;
            }
            else
            {
                stats.ShieldRegenRate += 5.0F;
            }
        };
    }
}