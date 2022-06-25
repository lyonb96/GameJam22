using Godot;
using System;

public class ShipPhysics : RigidBody2D
{
	private Ship Ship { get; set; }

	private float MoveSpeed { get; set; }

	private float Acceleration { get; set; }

	private float SlowRate { get; set; }

	private Vector2 FinalVelocity { get; set; }

	private WorldScript WorldScript { get; set; }

	private float RotationRate { get; set; }

	private float DesiredRotation { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
		Ship = GetParent<Ship>();
		MoveSpeed = 150.0F;
		Acceleration = 10.0F;
		SlowRate = 2.0F;
		RotationRate = 10.0F;
		DesiredRotation = 0.0F;
	}

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		base._IntegrateForces(state);
		state.LinearVelocity = FinalVelocity;
		state.AngularVelocity = DesiredRotation;
		DesiredRotation = 0.0F;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// Handle angular velocity
		if (!WorldScript.BuildMode)
		{
			var localMouse = GetLocalMousePosition();
			var dirToMouse = localMouse.Normalized();
			var mouseRotation = Mathf.Rad2Deg(dirToMouse.Angle());
			DesiredRotation += Utils.SmallestMagnitude(mouseRotation, mouseRotation * delta * RotationRate);
		}
		// Handle linear movement
		var currentVelocity = LinearVelocity;
		var desiredSpeed = 0.0F;
		if (Input.IsActionPressed("MoveForward"))
		{
			desiredSpeed = MoveSpeed;
		}
		else if (Input.IsActionPressed("MoveBackward"))
		{
			desiredSpeed = -MoveSpeed;
		}
		var desiredVelocity = new Vector2(desiredSpeed, 0.0F).Rotated(Rotation);
		var distanceTo = desiredVelocity - currentVelocity;
		FinalVelocity = currentVelocity + (distanceTo * delta * Acceleration);
	}
}
