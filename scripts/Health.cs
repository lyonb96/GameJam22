using Godot;
using System;

public class Health : Node2D
{
	private TextureProgress HealthbarReference { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthbarReference = this.GetChildNodeByName<TextureProgress>("Healthbar");
		//HealthbarReference.Value = 0;
	}
	
	public override void _Process(float delta)
	{
		var viewport = GetViewport().Size;
		Position = new Vector2(viewport.x / -2.0F, viewport.y / 2.0F - 110);
		GD.Print(Position);
	}

	public override void _PhysicsProcess(float delta)
	{
		if(HealthbarReference.Value < HealthbarReference.MaxValue) {
			HealthbarReference.Value += 0.01;
		}
	}

	public void changeHealth(float change)
	{
		HealthbarReference.Value += change;
	}

	public void changeMaxHealth(float change)
	{
		HealthbarReference.MaxValue += change;
	}
}
