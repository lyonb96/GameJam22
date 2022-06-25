using Godot;

public static class Utils
{
    public static TNodeType GetChildNodeByName<TNodeType>(this Node parent, string name)
        where TNodeType : Node
    {
        foreach (var child in parent.GetChildren())
        {
            if (!(child is Node childNode))
            {
                continue;
            }
            if (child is TNodeType childTNode && childTNode.Name == name)
            {
                return childTNode;
            }
            var namedChild = childNode.GetChildNodeByName<TNodeType>(name);
            if (namedChild != null)
            {
                return namedChild;
            }
        }
        return null;
    }

	public static float LargestMagnitude(params float[] inputs)
	{
		if (inputs.Length == 0)
		{
			return 0.0F;
		}
		float? winner = null;
		foreach (var input in inputs)
		{
			if (winner is null)
			{
				winner = input;
			}
			else if (Mathf.Abs(winner.Value) < Mathf.Abs(input))
			{
				winner = input;
			}
		}
		return winner.Value;
	}

	public static float SmallestMagnitude(params float[] inputs)
	{
		if (inputs.Length == 0)
		{
			return 0.0F;
		}
		float? winner = null;
		foreach (var input in inputs)
		{
			if (winner is null)
			{
				winner = input;
			}
			else if (Mathf.Abs(winner.Value) > Mathf.Abs(input))
			{
				winner = input;
			}
		}
		return winner.Value;
	}
}