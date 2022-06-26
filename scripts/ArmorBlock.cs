public class ArmorBlock : ShipBlock
{
	public ArmorBlock()
		: base()
	{
        AttachableSides = Utils.AllSides;
        // Armor blocks just add 50hp to the ship
        StatBlockMods = statBlock => statBlock.MaxHealth += 50.0F;
	}
}
