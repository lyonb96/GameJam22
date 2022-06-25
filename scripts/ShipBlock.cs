using System.Collections.Generic;
using Godot;

public enum Sides
{
    Top = 1,
    Bottom = 2,
    Left = 4,
    Right = 8,
}

public class ShipBlock : RigidBody2D
{
    private ShipBlock BlockParent { get; set; }

    private List<ShipBlock> BlockChildren { get; set; }

    private float MaxHealth { get; set; }

    private float CurrentHealth { get; set; }

    public Sides AttachableSides { get; protected set; }

    public Ship DraggingShip { get; set; }

    private CollisionShape2D BlockCollision { get; set; }

    public ShipBlock(float health)
    {
        MaxHealth = CurrentHealth = health;
        BlockChildren = new List<ShipBlock>();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is CollisionShape2D coll)
            {
                BlockCollision = coll;
                break;
            }
        }
    }

    public void AttachChild(ShipBlock child, Sides side)
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
        // Calculate where to attach the block
        var dir = GetDirectionForSide(side);
        child.QueueFree();

        AddChild(child);
    }

    public bool CanAttachAtSide(Sides side)
    {
        return (AttachableSides & side) > 0;
    }

    public Ship GetOwningShip()
    {
        var next = this;
        while (next != null && !(next is Ship))
        {
            next = next.BlockParent;
        }
        return next as Ship;
    }

    public bool IsPartOfPlayer()
    {
        return GetOwningShip() == WorldScript.Instance.PlayerShip;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (DraggingShip != null)
        {

        }
    }

    protected Vector2 GetDirectionForSide(Sides side)
    {
        switch (side)
        {
            case Sides.Top:
                return Vector2.Up;
            case Sides.Bottom:
                return Vector2.Down;
            case Sides.Left:
                return Vector2.Left;
            case Sides.Right:
                return Vector2.Right;
        }
        return new Vector2();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
