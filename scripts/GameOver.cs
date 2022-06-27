using Godot;
using System;

public class GameOver : Node2D
{
	public Label Score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Score = this.GetChildNodeByName<Label>("Score");
	}

	public void EndScreen()
	{
		Score.SetText("Your Score Was: " + WorldScript.Instance.PlayerScore.ToString());
	}
}
