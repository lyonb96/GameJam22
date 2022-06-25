using Godot;
using System;

public class ShipCamera : Camera2D
{
	private Ship TrackingShip { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (TrackingShip is null)
		{
			TrackingShip = WorldScript.Instance.PlayerShip;
			return;
		}
		Position = TrackingShip.Position;
	}
}
