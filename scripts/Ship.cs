using Godot;
using System;

public class Ship : Node2D
{
	// Stats
	public WorldScript WorldScript { get; set; }

	private ShipPhysics Physics { get; set; }

	public Vector2 PhysicsPosition => Physics.Position;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
		Physics = this.GetChildNodeByName<ShipPhysics>("ShipPhysics");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	}
}
