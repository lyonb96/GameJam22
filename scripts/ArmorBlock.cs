public class ArmorBlock : ShipBlock
{
	public ArmorBlock()
		: base(50.0F)
	{
		AttachableSides |= Sides.Top;
		AttachableSides |= Sides.Bottom;
		AttachableSides |= Sides.Left;
		AttachableSides |= Sides.Right;
	}
}
