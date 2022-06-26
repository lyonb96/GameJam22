public class ThrusterBlock : ShipBlock
{
    public ThrusterBlock()
    {
        AttachableSides = Sides.Top;
        StatMods = new PartStatMod
        {
            MoveSpeedMod = new StatBlockModifier { Amount = 100.0F, Mode = StatModMode.Flat },
            ChallengeRating = 5,
        };
        BlockName = "SonicThrust 5000";
        BlockDescription = "Gotta go fast";
    }
}