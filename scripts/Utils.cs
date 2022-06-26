using System.Collections.Generic;
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

	public static List<Sides> GetSides(Sides side)
	{
		var output = new List<Sides>();
		if ((side & Sides.Top) > 0)
		{
			output.Add(Sides.Top);
		}
		if ((side & Sides.Bottom) > 0)
		{
			output.Add(Sides.Bottom);
		}
		if ((side & Sides.Left) > 0)
		{
			output.Add(Sides.Left);
		}
		if ((side & Sides.Right) > 0)
		{
			output.Add(Sides.Right);
		}
		return output;
	}

	public static Sides AllSides
		=> Sides.Top | Sides.Bottom | Sides.Left | Sides.Right;

	private static PackedScene ProjectileScene;

	public static void SpawnLaser(float rotation, Vector2 location, Ship owner)
	{
		if (ProjectileScene is null)
		{
			ProjectileScene = (PackedScene)ResourceLoader.Load("res://scenes/Projectile.tscn");
		}
		var newProjectile = ProjectileScene.Instance() as Projectile;
		newProjectile.Rotation = rotation;
		newProjectile.GlobalPosition = location;
		newProjectile.IgnoreShip = owner;
		owner.GetTree().Root.AddChild(newProjectile);
	}
}
