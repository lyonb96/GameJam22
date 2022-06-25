using Godot;
using System;
using System.Linq;

public class Ship : ShipBlock
{
	// Stats
	public WorldScript WorldScript { get; set; }

	private ShipPhysics Physics { get; set; }

	private float MoveSpeed { get; set; }

	private float Acceleration { get; set; }

	private float SlowRate { get; set; }

	private Vector2 FinalVelocity { get; set; }

	private float RotationRate { get; set; }

	private float DesiredRotation { get; set; }

	private ShipBlock CurrentDraggedBlock { get; set; }

	public PackedScene Projectile { get; set; }

	public Ship()
		: base(100.0F)
	{
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
		Projectile = (PackedScene)ResourceLoader.Load("res://scenes/Projectile.tscn");
		WorldScript.PlayerShip = this;
		WorldScript.OnBuildModeChanged += OnBuildModeChanged;
		Physics = this.GetChildNodeByName<ShipPhysics>("ShipPhysics");
		MoveSpeed = 150.0F;
		Acceleration = 10.0F;
		SlowRate = 2.0F;
		RotationRate = 10.0F;
		DesiredRotation = 0.0F;
	}

	public override void _PhysicsProcess(float delta)
	{
		if (WorldScript.BuildMode && Input.IsActionJustPressed("Click"))
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
					CurrentDraggedBlock = block;
					CurrentDraggedBlock.DraggingShip = this;
				}
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
		// Handle angular velocity
		if (!WorldScript.BuildMode)
		{
			var localMouse = GetLocalMousePosition();
			var dirToMouse = localMouse.Normalized();
			var mouseRotation = Mathf.Rad2Deg(dirToMouse.Angle());
			DesiredRotation += Utils.SmallestMagnitude(mouseRotation, mouseRotation * delta * RotationRate);

			//Handle Firing
			if (Input.IsActionJustPressed("Click"))
			{
				var newProjectile = Projectile.Instance() as Projectile;
				newProjectile.Rotation = Rotation;
				newProjectile.GlobalPosition = GlobalPosition;
				newProjectile.IgnoreShip = this;
				Owner.AddChild(newProjectile);
			}
		}
		// Handle linear movement
		var currentVelocity = LinearVelocity;
		var desiredSpeed = 0.0F;
		if (Input.IsActionPressed("MoveForward"))
		{
			desiredSpeed = MoveSpeed;
		}
		else if (Input.IsActionPressed("MoveBackward"))
		{
			desiredSpeed = -MoveSpeed;
		}
		var desiredVelocity = new Vector2(desiredSpeed, 0.0F).Rotated(Rotation);
		var distanceTo = desiredVelocity - currentVelocity;
		FinalVelocity = currentVelocity + (distanceTo * delta * Acceleration);
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
		if (!buildMode && CurrentDraggedBlock != null)
		{
			CurrentDraggedBlock.DraggingShip = null;
			CurrentDraggedBlock = null;
		}
	}
}
