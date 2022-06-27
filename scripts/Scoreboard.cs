using Godot;
using System;

public class Scoreboard : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//   Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		this.GetChildNodeByName<Label>("Score").SetText("Score: " + WorldScript.Instance.PlayerScore);
		var viewport = GetViewport().Size;
		this.Position = new Vector2(viewport.x / 2.0F - 200F, viewport.y / -2.0F);
	}
}
