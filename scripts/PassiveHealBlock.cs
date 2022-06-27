public class PassiveHealBlock : ShipBlock
{
	public PassiveHealBlock()
		: base()
	{
		AttachableSides = Utils.AllSides;
		// Armor blocks just add 50hp to the ship
		StatMods = new PartStatMod
		{
			PassiveHealMod = new StatBlockModifier { Amount = 2.5F, Mode = StatModMode.Flat },
            MaxHealthMod = new StatBlockModifier { Amount = 25.0F, Mode = StatModMode.Flat },
			ChallengeRating = 5,
		};
		BlockName = "PMB™ Patchma";
		BlockDescription = "Patchma what?";
	}
}
