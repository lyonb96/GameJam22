using Godot;
using System;

public class Mode : Node2D
{
	private Sprite ActiveModeSprite { get; set; }
	
	private Sprite BuildModeSprite { get; set; }
	
	private Node ShipNode { get; set; }
	
	private Camera2D CameraView { get; set; }
	private float x { get; set; }
	private float y { get; set; }
	private float Speed { get; set; }
	private Vector2 TargetPosition { get; set; }

	private WorldScript WorldScript { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
		WorldScript.OnBuildModeChanged += mode =>
		{
			ActiveModeSprite.Visible = !mode;
			BuildModeSprite.Visible = mode;
			GD.Print(mode);
		};
		ActiveModeSprite = this.GetChildNodeByName<Sprite>("ActiveModeSprite");
		BuildModeSprite = this.GetChildNodeByName<Sprite>("BuildModeSprite");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
  	public override void _Process(float delta)
  	{
		var viewport = GetViewport().Size;
		this.Position = new Vector2(viewport.x / -2.0F, viewport.y / -2.0F);
  	}
}
