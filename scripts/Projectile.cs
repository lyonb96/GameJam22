using Godot;
using System;

public class Projectile : Node2D
{
	private float Speed { get; set; }
	public Ship IgnoreShip { get; set; }
	public float Damage { get; set; }
	public PackedScene AsteroidGroup { get; set; }
	private float TimeToLive { get; set; }
	private float TimeAlive { get; set; }

	public Projectile()
	{
		TimeToLive = 5.0F;
		TimeAlive = 0.0F;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Speed = 20F;
		AsteroidGroup = (PackedScene)ResourceLoader.Load("res://scenes/Asteroid.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		TimeAlive += delta;
		if (TimeAlive > TimeToLive)
		{
			// Prevent all the missed projectiles from floating forever and consuming resources
			QueueFree();
			return;
		}
		var Workspace = GetWorld2d().DirectSpaceState;
		var endPosition = new Vector2(Position.x + Speed * Mathf.Cos(Rotation), Position.y + Speed * Mathf.Sin(Rotation));
		var result = Workspace.IntersectRay(Position, endPosition, new Godot.Collections.Array { this, IgnoreShip });
		if(result.Count == 0)
		{
			Position = endPosition;
		}
		else
		{
			var collider = result["collider"];
			if (collider is IDamageable damageable)
			{
				damageable.TakeDamage(Damage);
			}
			QueueFree();
		}
	}
}
