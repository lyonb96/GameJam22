using Godot;
using System;

public class Projectile : Node2D
{
	private float Speed { get; set; }
	public Ship IgnoreShip { get; set; }

	public PackedScene AsteroidGroup { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Speed = 9F;
		AsteroidGroup = (PackedScene)ResourceLoader.Load("res://scenes/Asteroid.tscn");
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
			if(result["collider"] is RigidBody2D)
			{
				RigidBody2D Collider = (RigidBody2D)result["collider"];
				if(Collider is Asteroids)
				{
					Asteroids ColliderCast = (Asteroids)Collider;
					ColliderCast.Destroy();
				}
			}
			QueueFree();
		}
	}
}
