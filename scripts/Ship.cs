using Godot;
using System;

public class Ship : Node2D
{
	// Stats
	public float MoveSpeed { get; set; }

	// Max rotation speed
	public float RotationSpeed { get; set; }

	// Current Velocity
	public Vector2 LocalVelocity { get; set; }

	// Rotation Rate
	public float SlowRate { get; set; }	//Number from 0 - 1 which increases slow with a larger number

	public bool BuildMode { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LocalVelocity = Vector2.Zero;

		//Variable Initialization
		MoveSpeed = 150F;
		RotationSpeed = 6.0F;
		SlowRate = 2F;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// Handle Build Mode
		if (Input.IsActionJustPressed("SwitchMode"))
		{
			BuildMode = !BuildMode;
		}
		// Handle angular velocity
		if (!BuildMode)
		{
			var localMouse = GetLocalMousePosition();
			var dirToMouse = localMouse.Normalized();
			var mouseRotation = Mathf.Rad2Deg(dirToMouse.Angle());
			RotationDegrees += SmallestMagnitude(mouseRotation, mouseRotation * RotationSpeed * delta);
		}
		// Handle linear movement
		var hasLinearMovement = false;
		if (Input.IsActionPressed("MoveForward"))
		{
			LocalVelocity += new Vector2(delta * MoveSpeed, 0);
			hasLinearMovement = true;
		}
		if (Input.IsActionPressed("MoveBackward"))
		{
			LocalVelocity -= new Vector2(delta * MoveSpeed, 0);
			hasLinearMovement = true;
		}
		var length = LocalVelocity.LengthSquared();
		if (length > (MoveSpeed * MoveSpeed))
		{
			var direction = LocalVelocity.Normalized();
			LocalVelocity = direction * MoveSpeed;
		}
		if (!hasLinearMovement)
		{
			LocalVelocity = LocalVelocity * Math.Max(1 - (SlowRate * delta), 0.0F);
		}
		Position += LocalVelocity.Rotated(Rotation) * delta;
	}

	private float LargestMagnitude(params float[] inputs)
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
			else if (Math.Abs(winner.Value) < Math.Abs(input))
			{
				winner = input;
			}
		}
		return winner.Value;
	}

	private float SmallestMagnitude(params float[] inputs)
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
			else if (Math.Abs(winner.Value) > Math.Abs(input))
			{
				winner = input;
			}
		}
		return winner.Value;
	}
}
