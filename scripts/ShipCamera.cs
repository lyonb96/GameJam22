using Godot;
using System;

public class ShipCamera : Camera2D
{
	private Ship TrackingShip { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TrackingShip = GetTree().Root.GetChildNodeByName<Ship>("ShipNode2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Position = TrackingShip.PhysicsPosition;
	}
}
