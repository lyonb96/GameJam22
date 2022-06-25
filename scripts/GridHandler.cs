using System.Collections.Generic;
using Godot;

public class GridHandler
{
    private Ship Owner { get; set; }

    public GridHandler(Ship owner)
    {
        Owner = owner;
    }
}