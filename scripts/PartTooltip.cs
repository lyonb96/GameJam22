using Godot;
using System;

public class PartTooltip : Node2D
{
    public Label NameLabel { get; set; }
    public Label DescriptionLabel { get; set; }
    public Label StatLabel { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        NameLabel = this.GetChildNodeByName<Label>("NameLabel");
        DescriptionLabel = this.GetChildNodeByName<Label>("DescriptionLabel");
        StatLabel = this.GetChildNodeByName<Label>("StatLabel");
    }
    
    public void SetBlock(ShipBlock block)
    {
        if (block is null)
        {
            NameLabel.Text = string.Empty;
            DescriptionLabel.Text = string.Empty;
            StatLabel.Text = string.Empty;
            return;
        }
        GD.Print(block.BlockName);
        NameLabel.Text = block.BlockName;
        DescriptionLabel.Text = block.BlockDescription;
        if (block.StatMods != null)
        {
            StatLabel.Text = block.StatMods.GetSummary();
        }
    }
}
