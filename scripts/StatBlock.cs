using System;

public class StatBlock
{
    public delegate void StatChangeDelegate(float oldValue, float newValue);

    private float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            if (MaxHealth != value)
            {
                var oldValue = MaxHealth;
                maxHealth = value;
                MaxHealthChanged?.Invoke(oldValue, MaxHealth);
            }
        }
    }

    public event StatChangeDelegate MaxHealthChanged;

    public float MaxShield { get; set; }

    public float ShieldRegenRate { get; set; }

    public float MoveSpeed { get; set; }

    public float Acceleration { get; set; }

    public float RotationRate { get; set; }

    public float PassiveHealRate { get; set; }
}