using Godot;
using System;

public class Mode : Node2D
{
	private Sprite ActiveSprite { get; set; }
	
	private Sprite BuildModeSprite { get; set; }
	
	private Node ShipNode { get; set; }
	
	private Camera2D CameraView { get; set; }

	private bool BuildMode { get; set; }
	
	private float x { get; set; }
	private float y { get; set; }
	private float Speed { get; set; }
	private Vector2 TargetPosition { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ActiveSprite = this.GetChildNodeByName<Sprite>("ActiveModeSprite");
		BuildModeSprite = this.GetChildNodeByName<Sprite>("BuildModeSprite");
		//ShipNode = Scene.GetChildNodeByName<Node>("ShipNode2D");
		//CameraView = ShipNode.GetChildNodeByName<Camera2D>("Camera2D");
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
		x = GetTree().GetRoot().GetChildNodeByName<Node2D>("ShipNode2D").Position.x;
		y = GetTree().GetRoot().GetChildNodeByName<Node2D>("ShipNode2D").Position.y;
		this.Position = new Vector2(x + 150 - (GetViewport().Size.x/2), y + 150 - (GetViewport().Size.y/2));

  	}
}
