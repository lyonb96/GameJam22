using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class Ship : ShipBlock, IDamageable
{
	// Stats
	public WorldScript WorldScript { get; set; }
	
	public bool IsPlayer { get; set; }

	public StatBlock ShipStats { get; set; }

	private float SlowRate { get; set; }

	protected Vector2 FinalVelocity { get; set; }

	protected float DesiredRotation { get; set; }

	private ShipBlock CurrentDraggedBlock { get; set; }

	private Vector2? DraggedBlockSnapLocation { get; set; }

	public PackedScene Projectile { get; set; }

	public GridHandler GridHandler { get; set; }

	public Sprite HoverSprite { get; set; }

	protected List<WeaponLogic> Weapons { get; set; }

	private bool _shooting;
	protected bool Shooting
	{
		get => _shooting;
		set
		{
			if (Shooting != value)
			{
				_shooting = value;
				HandleShootingChange();
			}
		}
	}

	public float Health { get; protected set; }

	public float Shield { get; protected set; }

	public Ship()
		: base()
	{
		Weapons = new List<WeaponLogic>();
		IsPlayer = true;
		GridHandler = new GridHandler(this);
		GridHandler.AddBlock(new Vector2(), Utils.AllSides);
		ShipStats = new StatBlock
		{
			MaxHealth = 100.0F,
			MaxShield = 0.0F,
			ShieldRegenRate = 0.0F,
			MoveSpeed = 200.0F,
			Acceleration = 10.0F,
			RotationRate = 10.0F,
			PassiveHealRate = 2.5F,
		};
		Health = ShipStats.MaxHealth;
		Shield = ShipStats.MaxShield;
		ShipStats.MaxHealthChanged += (oldVal, newVal) =>
		{
			if (newVal > oldVal)
			{
				var newHealth = newVal - oldVal;
				Heal(newHealth);
			}
		};
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
		Projectile = (PackedScene)ResourceLoader.Load("res://scenes/Projectile.tscn");
		if (IsPlayer)
		{
			WorldScript.PlayerShip = this;
		}
		WorldScript.OnBuildModeChanged += OnBuildModeChanged;
		HoverSprite = this.GetChildNodeByName<Sprite>("HoverSpot");
		SlowRate = 2.0F;
		DesiredRotation = 0.0F;
	}

	public override void _PhysicsProcess(float delta)
	{
		if (!IsPlayer)
		{
			return;
		}
		if (WorldScript.BuildMode)
		{
			if (Input.IsActionJustPressed("Click"))
			{
				// Check for blocks near mouse
				var mousePosition = GetGlobalMousePosition();
				var spaceState = GetWorld2d().DirectSpaceState;
				var result = spaceState.IntersectPoint(mousePosition);
				// Check if any of the hit objects are ship parts that are not owned by another ship
				foreach (var hit in result)
				{
					var dict = hit as Godot.Collections.Dictionary;
					if (dict is null)
					{
						continue;
					}
					var collider = dict["collider"];
					if (collider is ShipBlock block && block.GetOwningShip() == null)
					{
						PickupBlock(block);
					}
				}
			}
			else if (Input.IsActionJustReleased("Click"))
			{
				DropBlock();
			}
		}
	}

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		base._IntegrateForces(state);
		state.LinearVelocity = FinalVelocity;
		state.AngularVelocity = DesiredRotation;
		DesiredRotation = 0.0F;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		foreach (var weapon in Weapons)
		{
			weapon.Update(delta);
		}
		if (!IsPlayer)
		{
			return;
		}
		// Player passively regens health
		Heal(delta * ShipStats.PassiveHealRate);
		// Handle angular velocity
		if (!WorldScript.BuildMode)
		{
			var localMouse = GetLocalMousePosition();
			var dirToMouse = localMouse.Normalized();
			var mouseRotation = Mathf.Rad2Deg(dirToMouse.Angle());
			DesiredRotation += Utils.SmallestMagnitude(mouseRotation, mouseRotation * delta * ShipStats.RotationRate);

			//Handle Firing
			Shooting = Input.IsActionPressed("Click");
			// if (Input.IsActionJustPressed("Click"))
			// {
			//  Shooting = true;
			// 	var newProjectile = Projectile.Instance() as Projectile;
			// 	newProjectile.Rotation = Rotation;
			// 	newProjectile.GlobalPosition = GlobalPosition;
			// 	newProjectile.IgnoreShip = this;
			// 	Owner.AddChild(newProjectile);
			// }
		}
		// Handle linear movement
		var currentVelocity = LinearVelocity;
		var desiredSpeed = new Vector2();
		if (Input.IsActionPressed("MoveForward"))
		{
			desiredSpeed.x = ShipStats.MoveSpeed;
		}
		else if (Input.IsActionPressed("MoveBackward"))
		{
			desiredSpeed.x = -ShipStats.MoveSpeed;
		}
		if (Input.IsActionPressed("MoveRight"))
		{
			desiredSpeed.y = ShipStats.MoveSpeed * 0.5F;
		}
		else if (Input.IsActionPressed("MoveLeft"))
		{
			desiredSpeed.y = ShipStats.MoveSpeed * -0.5F;
		}
		var desiredVelocity = desiredSpeed.Rotated(Rotation);
		var distanceTo = desiredVelocity - currentVelocity;
		FinalVelocity = currentVelocity + (distanceTo * delta * ShipStats.Acceleration);

		if (WorldScript.BuildMode && CurrentDraggedBlock != null)
		{
			HandleBlockDragging();
		}
	}

	public void AddNewCollision(
		CollisionShape2D coll,
		Vector2 attachPosition,
		float attachRotation)
	{
		AddChild(coll);
		coll.Rotation = attachRotation;
		coll.Position = attachPosition;
	}

	protected void OnBuildModeChanged(bool buildMode)
	{
		if (buildMode)
		{
			Shooting = false;
		}
		if (!buildMode)
		{
			DropBlock();
		}
	}

	protected void PickupBlock(ShipBlock block)
	{
		if (CurrentDraggedBlock != null)
		{
			DropBlock();
		}
		CurrentDraggedBlock = block;
		CurrentDraggedBlock.DraggingShip = this;
		// CurrentDraggedBlock.Mode = ModeEnum.Kinematic;
	}

	protected void DropBlock()
	{
		if (CurrentDraggedBlock is null)
		{
			return;
		}
		if (DraggedBlockSnapLocation != null)
		{
			// Attach the block
			AttachChild(CurrentDraggedBlock, DraggedBlockSnapLocation.Value * 32.0F);
			GridHandler.AddBlock(DraggedBlockSnapLocation.Value, Utils.AllSides);
			HoverSprite.Visible = false;
		}
		CurrentDraggedBlock.DraggingShip = null;
		// CurrentDraggedBlock.Mode = ModeEnum.Rigid;
		CurrentDraggedBlock = null;
	}

	protected void HandleBlockDragging()
	{
		var blockPosition = CurrentDraggedBlock.GlobalPosition;
		var closest = GridHandler.FindNearestOpenBlock(blockPosition);
		var closestWorld = ToGlobal(closest * 32.0F);
		if (closestWorld.DistanceSquaredTo(blockPosition) <= 64.0F * 64.0F)
		{
			DraggedBlockSnapLocation = closest;
			HoverSprite.Visible = true;
			HoverSprite.Position = DraggedBlockSnapLocation.Value * 32.0F;
		}
		else
		{
			DraggedBlockSnapLocation = null;
			HoverSprite.Visible = false;
		}
	}

	public void AttachChild(ShipBlock child, Vector2 location)
	{
		// if (!CanAttachAtSide(side))
		// {
		//     GD.Print("Can't attach at that side");
		//     return;
		// }
		var owningShip = GetOwningShip();
		if (owningShip is null)
		{
			GD.Print("Can't attach parts to unowned ships");
			return;
		}
		if (child.StatMods != null)
		{
			ShipStats.ApplyPartMods(child.StatMods);
		}
		if (child is WeaponBlock weapon)
		{
			var weaponLogic = weapon.Logic;
			if (weaponLogic != null)
			{
				weaponLogic.OwningShip = this;
				weaponLogic.Location = location;
				Weapons.Add(weaponLogic);
			}
		}
		// Attach it to the ship
		child.RemoveChild(child.BlockCollision);
		owningShip.AddNewCollision(
			child.BlockCollision,
			location,
			0.0F);
		// Delete the old block
		child.QueueFree();
	}

	public void HandleShootingChange()
	{
		// Find all weapons and inform them of the shoosting
		foreach (var weapon in Weapons)
		{
			if (Shooting)
			{
				weapon.StartShooting();
			}
			else
			{
				weapon.StopShooting();
			}
		}
	}

	public void TakeDamage(float damage)
	{
		var remaining = damage;
		if (Shield > 0)
		{
			Shield -= damage;
			if (Shield < 0.0F)
			{
				remaining = -Shield;
				Shield = 0.0F;
			}
		}
		Health -= remaining;
		if (Health <= 0.0F)
		{
			// Dead-ass
			Die();
		}
	}

	public float GetMaxHealth()
	{
		return ShipStats.MaxHealth;
	}

	public float GetCurrentHealth()
	{
		return Health;
	}

	public void Heal(float amount)
	{
		Health = Mathf.Min(GetMaxHealth(), Health + amount);
	}

	protected virtual void Die()
	{
		if (IsQueuedForDeletion())
		{
			return;
		}
		Explosion explosion = Utils.ExplosionScene.Instance() as Explosion;
		explosion.Position = GlobalPosition;
		explosion.GetChildNodeByName<Particles2D>("ShipExplosion").Emitting = true;
		explosion.GetChildNodeByName<Particles2D>("AsteroidExplosion").Visible = false;
		explosion.changeScoreText(this.ShipStats.ChallengeRating);

		WorldScript.AddChild(explosion);
		GD.Print("Challenge Rating: " + this.ShipStats.ChallengeRating);
		if (IsPlayer)
		{
			WorldScript.PlayerShip = null;
			GetTree().GetRoot().GetChildNodeByName<Node2D>("GameOver").Visible = true;
		}
		QueueFree();
	}
}
