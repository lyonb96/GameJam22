using Godot;
using System;

public class Ship : Node2D
{
	// Stats
	public float MoveSpeed { get; set; }

	// Velocity
	public Vector2 Velocity { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Velocity = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var hasInput = false;
		if (Input.IsActionPressed("MoveForward"))
		{
			Velocity += new Vector2(delta * MoveSpeed, 0);
			hasInput = true;
		}
		if (Input.IsActionPressed("MoveBackward"))
		{
			Velocity -= new Vector2(delta * MoveSpeed, 0);
			hasInput = true;
		}
		var length = Velocity.LengthSquared();
		if (length > (MoveSpeed * MoveSpeed))
		{
			var direction = Velocity.Normalized();
			Velocity = direction * MoveSpeed;
		}
		if (!hasInput)
		{
			Velocity = Velocity * 0.75F * delta;
		}
		Position += Velocity * delta;
	}
}
