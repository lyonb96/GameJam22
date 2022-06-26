using Godot;
using System;

public class WorldScript : Node2D
{
	public static WorldScript Instance { get; private set; }

	public bool BuildMode { get; private set; }

	public Ship PlayerShip { get; set; }

	public PackedScene EnemyScene { get; set; }

	public PartTooltip Tooltip { get; set; }

	public delegate void OnBuildModeChangedDelegate(bool mode);
	public event OnBuildModeChangedDelegate OnBuildModeChanged;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EnemyRegistry.Initialize();
		Instance = this;
		EnemyScene = ResourceLoader.Load("res://scenes/Enemy.tscn") as PackedScene;
		var enemy = EnemyScene.Instance() as EnemyAI;
		enemy.GlobalPosition = new Vector2(500.0F, 500.0F);
		enemy.SetSchema("Basic");
		AddChild(enemy);

		Tooltip = this.GetChildNodeByName<PartTooltip>("Tooltip");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// Handle Build Mode
		if (Input.IsActionJustPressed("SwitchMode"))
		{
			BuildMode = !BuildMode;
			OnBuildModeChanged?.Invoke(BuildMode);
		}
		HandleTooltip();
	}

	private void HandleTooltip()
	{
		// Check for blocks and stuff
		if (BuildMode)
		{
			// Check for blocks near mouse
			var mousePosition = GetGlobalMousePosition();
			var spaceState = GetWorld2d().DirectSpaceState;
			var result = spaceState.IntersectPoint(mousePosition);
			foreach (var hit in result)
			{
				if (hit is Godot.Collections.Dictionary dict
					&& dict["collider"] is ShipBlock block
					&& !(block is Ship)
					&& block.DraggingShip is null)
				{
					Tooltip.Visible = true;
					Tooltip.GlobalPosition = GetGlobalMousePosition();
					Tooltip.SetBlock(block);
					return;
				}
			}
		}
		Tooltip.SetBlock(null);
		Tooltip.Visible = false;
	}

	public void OnPlayerKill() {
		PlayerShip = null;
		//Game Over
		var GameOver = this.GetChildNodeByName<GameOver>("GameOver");
		GameOver.Visible = true;
		//Score Display
	}
}
