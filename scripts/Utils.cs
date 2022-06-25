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
}
