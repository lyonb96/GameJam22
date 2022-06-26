using Godot;
using System;

public class Explosion : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	//Timer EmitTimer;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(2.0F), "timeout");
		QueueFree();
	}

	public void changeScoreText(int Score)
	{
		Label ShipScore = this.GetChildNodeByName<Label>("ShipScore");
		ShipScore.SetText("+" + Score);
	}
}
