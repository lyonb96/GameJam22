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

	private Vector2? DraggedBlockSnapLocation { get; set; }

	public PackedScene Projectile { get; set; }

	public GridHandler GridHandler { get; set; }

	public Sprite HoverSprite { get; set; }

	public Ship()
		: base(100.0F)
	{
		GridHandler = new GridHandler(this);
		GridHandler.AddBlock(new Vector2(), Utils.AllSides);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldScript = GetTree().Root.GetChildNodeByName<WorldScript>("Scene");
		Projectile = (PackedScene)ResourceLoader.Load("res://scenes/Projectile.tscn");
		WorldScript.PlayerShip = this;
		WorldScript.OnBuildModeChanged += OnBuildModeChanged;
		Physics = this.GetChildNodeByName<ShipPhysics>("ShipPhysics");
		HoverSprite = this.GetChildNodeByName<Sprite>("HoverSpot");
		MoveSpeed = 150.0F;
		Acceleration = 10.0F;
		SlowRate = 2.0F;
		RotationRate = 10.0F;
		DesiredRotation = 0.0F;
	}

	public override void _PhysicsProcess(float delta)
	{
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
		// Attach it to the ship
		child.RemoveChild(child.BlockCollision);
		owningShip.AddNewCollision(
			child.BlockCollision,
			location,
			0.0F);
		// Delete the old block
		child.QueueFree();
	}
}
