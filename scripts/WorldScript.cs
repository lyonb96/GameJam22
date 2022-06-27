using System;
using System.Linq;
using Godot;

public class WorldScript : Node2D
{
	public static WorldScript Instance { get; private set; }

	public bool BuildMode { get; private set; }

	public Ship PlayerShip { get; set; }

	public PackedScene EnemyScene { get; set; }

	public PackedScene AudioScene { get; set; }
	public Audio AudioPlayer { get; set; }

	private float TimeSinceLastSpawn { get; set; }

	public PartTooltip Tooltip { get; set; }

	public delegate void OnBuildModeChangedDelegate(bool mode);
	public event OnBuildModeChangedDelegate OnBuildModeChanged;

	public int PlayerScore { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EnemyRegistry.Initialize();
		Instance = this;
		EnemyScene = ResourceLoader.Load("res://scenes/Enemy.tscn") as PackedScene;
		// var enemy = EnemyScene.Instance() as EnemyAI;
		// enemy.GlobalPosition = new Vector2(8000.0F, 8000.0F);
		// enemy.SetSchema("Basic");
		// AddChild(enemy);
		TimeSinceLastSpawn = 5.0F;

		Tooltip = this.GetChildNodeByName<PartTooltip>("Tooltip");

		AudioScene = (PackedScene)ResourceLoader.Load("res://scenes/Audio/Audio.tscn");
		AudioPlayer = AudioScene.Instance() as Audio;
		AddChild(AudioPlayer);
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
		HandleSpawning(delta);
	}

	public void OnPlayerDestroyedEnemy(EnemyAI enemy)
	{
		var challengeRating = enemy?.ShipStats?.ChallengeRating;
		if (challengeRating != null)
		{
			PlayerScore += challengeRating.Value;
			//GD.Print("Added " + challengeRating.Value + ". Score is now: " + PlayerScore);
		}
	}

	public void OnPlayerDie()
	{
		PlayerShip = null;
		GameOver EndScreen = this.GetChildNodeByName<GameOver>("GameOver");
		EndScreen.Visible = true;
		EndScreen.EndScreen();
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

	private void HandleSpawning(float delta)
	{
		if (PlayerShip is null)
		{
			return;
		}
		TimeSinceLastSpawn += delta;
		if (TimeSinceLastSpawn > 15.0F)
		{
			TimeSinceLastSpawn -= 15.0F;
			SpawnNewEnemy();
		}
	}

	private void SpawnNewEnemy()
	{
		var crLimit = Mathf.Max((float)PlayerShip.ShipStats.ChallengeRating * 1.2F, 10);
		var minCr = Mathf.Min(crLimit * 0.5F, 20);
		// Roll enemies that are less than or up to 20% greater in CR than the player
		var enemies = EnemyRegistry.Enemies
			.Where(x => x.Value.ChallengeRating <= crLimit && x.Value.ChallengeRating >= minCr)
			.Select(x => x.Key);
		// Select a random direction to spawn the enemy
		var random = new Random();
		var distance = 1500.0F + random.NextDouble() * 500.0F;
		var angle = -180.0F + (random.NextDouble() * 360.0F);
		var dir = Vector2.Up.Rotated(Mathf.Deg2Rad((float)angle));
		var spawnPos = dir.Normalized() * (float)distance;
		var schema = string.Empty;
		if (enemies.Count() == 1)
		{
			// If there's only one, just spawn that one
			schema = enemies.First();
		}
		else
		{
			// Select one at random
			schema = enemies.ElementAt(random.Next(enemies.Count()));
		}
		var enemy = EnemyScene.Instance() as EnemyAI;
		enemy.SetSchema(schema);
		enemy.GlobalPosition = PlayerShip.GlobalPosition + spawnPos;
		AddChild(enemy);
	}
}
