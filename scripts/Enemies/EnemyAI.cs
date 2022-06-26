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
			}
			if (block != null)
			{
				WorldScript.Instance.AddChild(block);
				AttachChild(block, part.Position * 32.0F);
				GD.Print("Attaching to ship ", block);
			}
		}
	}

	public override void _Process(float delta)
	{
		
	}
}
