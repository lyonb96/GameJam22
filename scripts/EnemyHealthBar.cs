using Godot;
using System;

public class EnemyHealthBar : Node2D
{
	private TextureProgress Healthbar { get; set; }
	
	public Ship AttachedShip { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Healthbar = this.GetChildNodeByName<TextureProgress>("Healthbar");
	}

//   Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Healthbar.MaxValue = AttachedShip.GetMaxHealth();
		Healthbar.Value = AttachedShip.Health;
		Position = AttachedShip.Position + new Vector2(-75, -100);
	}
}
