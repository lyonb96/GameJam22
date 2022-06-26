using Godot;
using System;

public class WorldScript : Node2D
{
	public static WorldScript Instance { get; private set; }

	public bool BuildMode { get; private set; }

	public Ship PlayerShip { get; set; }

	public PackedScene EnemyScene { get; set; }

	public delegate void OnBuildModeChangedDelegate(bool mode);
	public event OnBuildModeChangedDelegate OnBuildModeChanged;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EnemyRegistry.Initialize();
		Instance = this;
		EnemyScene = ResourceLoader.Load("res://scenes/Enemy.tscn") as PackedScene;
		var enemy = EnemyScene.Instance() as EnemyAI;
		enemy.GlobalPosition = new Vector2(8000.0F, 8000.0F);
		enemy.SetSchema("Basic");
		AddChild(enemy);
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
	}
}
