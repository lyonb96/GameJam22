// using Godot;
// using System;

// public class ShipDrag : RigidBody2D
// {
// 	private WorldScript WorldScript { get; set; }
	
// 	private bool Dragging { get; set; }

// 	private float MoveSpeed { get; set; }

// 	private float Acceleration { get; set; }

// 	private float SlowRate { get; set; }

// 	private Vector2 FinalVelocity { get; set; }

// 	private float RotationRate { get; set; }

// 	private float DesiredRotation { get; set; }

// 	// Called when the node enters the scene tree for the first time.
// 	public override void _Ready()
// 	{
// 		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
// 		Dragging = false;
// 		MoveSpeed = 300.0F;
// 		Acceleration = 30.0F;
// 		SlowRate = 5.0F;
// 		RotationRate = 15.0F;
// 		DesiredRotation = 0.0F;
// 	}

// 	public override void _IntegrateForces(Physics2DDirectBodyState state)
// 	{
// 		base._IntegrateForces(state);
// 		state.LinearVelocity = FinalVelocity;
// 		state.AngularVelocity = DesiredRotation;
// 		DesiredRotation = 0.0F;
// 	}
	
// 	public override void _Process(float delta)
// 	{
// 		//if (Dragging)
// 		//{
// 			//this.Position = GetLocalMousePosition();
			
// 			//Linear Velocity
// 			var currentVelocity = LinearVelocity;
// 			var desiredSpeed = 0.0F;
// 			if (Dragging)
// 			{
// 				var localMouse = GetLocalMousePosition();
// 				var dirToMouse = localMouse.Normalized();
// 				var mouseRotation = Mathf.Rad2Deg(dirToMouse.Angle());
// 				DesiredRotation += Utils.SmallestMagnitude(mouseRotation, mouseRotation * delta * RotationRate);
// 				desiredSpeed = MoveSpeed;
// 			}
// 			var desiredVelocity = new Vector2(desiredSpeed, 0.0F).Rotated(Rotation);
// 			var distanceTo = desiredVelocity - currentVelocity;
// 			FinalVelocity = currentVelocity + (distanceTo * delta * Acceleration);
// 		//}
// 		if (!Input.IsActionPressed("Click")) {
// 			Dragging = false;
// 		}
// 	}

// 	// Called once for every event, before _unhandled_input(), allowing you to consume some events.
// 	public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
// 	{
// 		if (Input.IsActionPressed("Click") && WorldScript.BuildMode)
// 		{
// 			Dragging = true;
// 		}
// 	}
	
// 	private void _on_ShipPhysics_body_entered(RigidBody2D body)
// 	{
// 		if (body is ShipPhysics shipPhys)
// 		{
// 			GD.Print("Hit something with ship physics on it");
// 		}
// 		if(body.GetParent().Name != null && body.GetParent().Name.Equals("ShipNode2D"))
// 		{
// 			GD.Print(body.GetParent().Name);
// 		}
// 	}
// }
