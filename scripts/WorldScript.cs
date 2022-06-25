using Godot;
using System;

public class WorldScript : Node2D
{
	public static WorldScript Instance { get; private set; }

	public bool BuildMode { get; private set; }

	public Ship PlayerShip { get; set; }

	public delegate void OnBuildModeChangedDelegate(bool mode);
	public event OnBuildModeChangedDelegate OnBuildModeChanged;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
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
