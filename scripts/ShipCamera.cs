using Godot;
using System;

public class ShipCamera : Camera2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (WorldScript.Instance.PlayerShip is null)
		{
			return;
		}
		Position = WorldScript.Instance.PlayerShip.Position;
	}
}
