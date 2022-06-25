using Godot;
using System;

public class Projectile : Node2D
{
	private float Speed { get; set; }
	public Ship IgnoreShip { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Speed = 9F;
		GD.Print(Rotation);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		var Workspace = GetWorld2d().DirectSpaceState;
		var endPosition = new Vector2(Position.x + Speed * Mathf.Cos(Rotation), Position.y + Speed * Mathf.Sin(Rotation));
		var result = Workspace.IntersectRay(Position, endPosition, new Godot.Collections.Array { this, IgnoreShip });
		if(result.Count == 0)
		{
			Position = endPosition;
		}
		else
		{
			QueueFree();
		}
	}
}
