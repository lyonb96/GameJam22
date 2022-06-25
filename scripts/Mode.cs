using Godot;
using System;

public class Mode : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public string currentTexture {get; set; }
	string ActiveTexture = "res://.import/Active.png-131dd35a7ccb90721add7fe1a29799a9.stex";
	string BuildTexture = "res://.import/Build.png-b8358e5ccc53e6e80d86b3987e196622.stex";
	
	private Sprite ActiveSprite { get; set; }

	private Sprite BuildModeSprite { get; set; }

	private bool BuildMode { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ActiveSprite = this.GetChildNodeByName<Sprite>("ActiveModeSprite");
		BuildModeSprite = this.GetChildNodeByName<Sprite>("BuildModeSprite");
		BuildMode = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
  	public override void _Process(float delta)
  	{
		if (Input.IsActionJustPressed("SwitchMode"))
		{
			BuildMode = !BuildMode;
			ActiveSprite.Visible = !BuildMode;
			BuildModeSprite.Visible = BuildMode;
		}
  	}
}
