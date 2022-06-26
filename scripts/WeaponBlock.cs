using Godot;

public class WeaponBlock : ShipBlock
{
    public WeaponLogic Logic { get; set; }
}

public abstract class WeaponLogic
{
    public Vector2 Location { get; set; }

    public Ship OwningShip { get; set; }

    public abstract void Update(float delta);

    public abstract void StartShooting();

    public abstract void StopShooting();
}