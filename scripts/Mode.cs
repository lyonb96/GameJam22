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
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentTexture = BuildTexture;
		GD.Print("Hello");
		GD.Print(this.get_Node("Sprite"));
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	//if (Input.IsActionJustPressed("SwitchMode") && currentTexture == BuildTexture)
	//{
	//	this.get_node("Sprite").Texture = ActiveTexture;
	//}
	//else if (Input.IsActionJustPressed("SwitchMode") && currentTexture == ActiveTexture)
	//{
	//	this.get_node("Sprite").Texture = BuildTexture;
	//}
  }
}
