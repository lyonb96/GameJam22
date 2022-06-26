using System;
using System.Text;

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

    public int ChallengeRating { get; set; }

    public void ApplyPartMods(PartStatMod mods)
    {
        float Calculate(float inValue, StatBlockModifier mod)
        {
            if (mod is null)
            {
                return inValue;
            }
            if (mod.Mode == StatModMode.Flat)
            {
                return inValue + mod.Amount;
            }
            else
            {
                return inValue * ((100.0F + mod.Amount) / 100.0F);
            }
        }
        MaxHealth = Calculate(MaxHealth, mods.MaxHealthMod);
        MaxShield = Calculate(MaxShield, mods.MaxShieldMod);
        ShieldRegenRate = Calculate(ShieldRegenRate, mods.ShieldRegenMod);
        MoveSpeed = Calculate(MoveSpeed, mods.MoveSpeedMod);
        PassiveHealRate = Calculate(PassiveHealRate, mods.PassiveHealMod);
        ChallengeRating += mods.ChallengeRating;
    }
}

public enum StatModMode
{
    Flat,
    Percent,
}

public class StatBlockModifier
{
    public float Amount { get; set; }

    public StatModMode Mode { get; set; }
}

public class PartStatMod
{
    public StatBlockModifier MaxHealthMod { get; set; }

    public StatBlockModifier MaxShieldMod { get; set; }

    public StatBlockModifier ShieldRegenMod { get; set; }

    public StatBlockModifier MoveSpeedMod { get; set; }

    public StatBlockModifier PassiveHealMod { get; set; }

    public int ChallengeRating { get; set; }

    public string GetSummary()
    {
        var output = new StringBuilder();
        void AppendAmount(StatBlockModifier mod)
        {
            if (mod.Mode == StatModMode.Flat)
            {
                output.AppendLine(mod.Amount.ToString());
            }
            else
            {
                output.AppendLine($"{mod.Amount}%");
            }
        }
        if (MaxHealthMod != null)
        {
            output.Append("Health: ");
            AppendAmount(MaxHealthMod);
        }
        if (MaxShieldMod != null)
        {
            output.Append("Shield: ");
            AppendAmount(MaxShieldMod);
        }
        if (ShieldRegenMod != null)
        {
            output.Append("Shield Regen: ");
            AppendAmount(ShieldRegenMod);
        }
        if (MoveSpeedMod != null)
        {
            output.Append("Move Speed: ");
            AppendAmount(MoveSpeedMod);
        }
        if (PassiveHealMod != null)
        {
            output.Append("Passive Regen: ");
            AppendAmount(PassiveHealMod);
        }
        return output.ToString();
    }
}