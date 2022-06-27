using Godot;

public class ShieldBlock : ShipBlock
{
    public ShieldBlock()
    {
        AttachableSides = Utils.AllSides;
        StatMods = new PartStatMod
        {
            MaxShieldMod = new StatBlockModifier { Amount = 50.0F, Mode = StatModMode.Flat },
            ShieldRegenMod = new StatBlockModifier { Amount = 10.0F, Mode = StatModMode.Percent },
        };
        BlockName = "Deflect-o-matic";
        BlockDescription = "Wanna see me deflect some damage? Wanna see me do it again?";
    }
}