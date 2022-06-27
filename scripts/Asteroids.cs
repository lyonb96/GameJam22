using Godot;
using System;

public class Asteroids : RigidBody2D, IDamageable
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public PackedScene ExplosionScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ExplosionScene = (PackedScene)ResourceLoader.Load("res://scenes/Explosion.tscn");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void TakeDamage(float damage)
	{
		// Asteroid gets destroyed if it takes any damage at all
		Destroy();
	}

	public float GetMaxHealth()
	{
		return 0.0F;
	}

	public float GetCurrentHealth()
	{
		return 0.0F;
	}

	public void Destroy()
	{
		Explosion NewExplosion = ExplosionScene.Instance() as Explosion;
		NewExplosion.Position = Position;
		NewExplosion.GetChildNodeByName<Particles2D>("AsteroidExplosion").Emitting = true;
		Owner.AddChild(NewExplosion);
		WorldScript.Instance.PlayerScore += 5;
		QueueFree();
	}
}
