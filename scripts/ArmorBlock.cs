public class ArmorBlock : ShipBlock
{
	public ArmorBlock()
		: base()
	{
		AttachableSides = Utils.AllSides;
		// Armor blocks just add 50hp to the ship
		StatMods = new PartStatMod
		{
			MaxHealthMod = new StatBlockModifier { Amount = 50.0F, Mode = StatModMode.Flat },
			ChallengeRating = 1,
		};
		BlockName = "FlexArmor, a product by Phil Swift";
		BlockDescription = "The easy way to patch, bond, seal and repair!";
	}
}
