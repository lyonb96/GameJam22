using Godot;
using System;

public class Health : Node2D
{
	private TextureProgress HealthbarReference { get; set; }
	private TextureProgress ShieldBarReference { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthbarReference = this.GetChildNodeByName<TextureProgress>("Healthbar");
		ShieldBarReference = this.GetChildNodeByName<TextureProgress>("ShieldBar");
		//HealthbarReference.Value = 0;
	}
	
	public override void _Process(float delta)
	{
		var viewport = GetViewport().Size;
		Position = new Vector2(viewport.x / -2.0F, viewport.y / 2.0F - 110);
		var ship = WorldScript.Instance.PlayerShip;
		if (ship != null)
		{
			HealthbarReference.MaxValue = ship.GetMaxHealth();
			HealthbarReference.Value = ship.GetCurrentHealth();
			if (ship.ShipStats.MaxShield > 0)
			{
				ShieldBarReference.Visible = true;
				ShieldBarReference.MaxValue = ship.ShipStats.MaxShield;
				ShieldBarReference.Value = ship.Shield;
			}
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
