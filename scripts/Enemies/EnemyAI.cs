using System.Collections.Generic;
using Godot;

public class EnemySchema
{
	public string Name { get; set; }

	public List<EnemyPart> Parts { get; set; }
}

public class EnemyPart
{
	public Vector2 Position { get; set; }

	public string Type { get; set; }
}

public class EnemyAI : Ship
{
	private EnemySchema Schema { get; set; }

	public EnemyAI()
	{
		IsPlayer = false;
	}

	public void SetSchema(string schemaName)
	{
		Schema = EnemyRegistry.Enemies[schemaName];
	}

	public override void _Ready()
	{
		base._Ready();
		ShipBlockRegistry.EnsureLoaded();
		if (Schema is null)
		{
			return;
		}
		foreach (var part in Schema.Parts)
		{
			ShipBlock block = null;
			switch (part.Type)
			{
				case "Core":
					break;
				case "Armor":
					block = ShipBlockRegistry.ArmorBlock.Instance() as ArmorBlock;
					break;
				case "Thruster":
					block = ShipBlockRegistry.ThrusterBlock.Instance() as ThrusterBlock;
					break;
				case "LaserCannon":
					block = ShipBlockRegistry.LaserCannonBlock.Instance() as LaserCannonBlock;
					break;
			}
			if (block != null)
			{
				WorldScript.Instance.AddChild(block);
				AttachChild(block, part.Position * 32.0F);
			}
		}
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		// AI wants to preserve a certain distance between it and the player
		// Movement
		var desiredMinDistance = 250.0F;
		var desiredMaxDistance = 400.0F;
		var player = WorldScript.Instance.PlayerShip;
		var directionToPlayer = (player.GlobalPosition - GlobalPosition).Normalized();
		var distance = player.GlobalPosition.DistanceTo(GlobalPosition);
		var desiredMoveDir = new Vector2();
		var desiredLookDirection = new Vector2();
		if (distance > desiredMaxDistance)
		{
			desiredMoveDir = directionToPlayer;
			desiredLookDirection = directionToPlayer;
		}
		else if (distance < desiredMinDistance)
		{
			desiredMoveDir = -directionToPlayer;
			desiredLookDirection = -directionToPlayer;
		}
		else
		{
			// In the safe zone, move perpindicular
			desiredMoveDir = directionToPlayer.Rotated(Mathf.Deg2Rad(90.0F)) * 0.5F;
			desiredLookDirection = directionToPlayer;
		}
		var currentVelocity = LinearVelocity;
		var desiredVelocity = desiredMoveDir * ShipStats.MoveSpeed;
		var distanceTo = desiredVelocity - currentVelocity;
		FinalVelocity = currentVelocity + (distanceTo * delta * ShipStats.Acceleration);
		// Rotation
		var playerRotation = Mathf.Rad2Deg(desiredLookDirection.Angle());
		var myRotation = RotationDegrees;
		if (playerRotation > myRotation)
		{
			DesiredRotation += 10.0F * delta * ShipStats.RotationRate;
		}
		else
		{
			DesiredRotation -= 10.0F * delta * ShipStats.RotationRate;
		}
		// Shooting
		var myAngle = Vector2.Right.Rotated(GlobalRotation);
		var dot = myAngle.Dot(directionToPlayer);
		if (dot > 0.85F && distance < 1000.0F)
		{
			// Try to shoot
			Shooting = true;
		}
		else
		{
			// Do not try to shoot
			Shooting = false;
		}
	}
}
