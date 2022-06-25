using Godot;

public static class Utils
{
    public static TNodeType GetChildNodeByName<TNodeType>(this Node parent, string name)
        where TNodeType : Node
    {
        foreach (var child in parent.GetChildren())
        {
            if (child is TNodeType childNode && childNode.Name == name)
            {
                return childNode;
            }
        }
        return null;
    }
}