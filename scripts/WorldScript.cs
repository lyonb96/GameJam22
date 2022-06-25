using Godot;
using System;

public class WorldScript : Node2D
{
    public bool BuildMode { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
		// Handle Build Mode
		if (Input.IsActionJustPressed("SwitchMode"))
		{
			BuildMode = !BuildMode;
		}
    }
}
