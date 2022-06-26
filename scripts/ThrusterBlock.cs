public class ThrusterBlock : ShipBlock
{
    public ThrusterBlock()
    {
        AttachableSides = Sides.Top;
        StatBlockMods = statBlock => statBlock.MoveSpeed += 100.0F;
    }
}