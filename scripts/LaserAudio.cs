using Godot;
using System;

public class LaserAudio : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		this.GetChildNodeByName<AudioStreamPlayer>("LaserAudio").Playing = true;
		await ToSignal(GetTree().CreateTimer(0.6F), "timeout");
		QueueFree();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
