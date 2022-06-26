using System.Collections.Generic;
using System.Linq;
using Godot;

public class GridHandler
{
    private Ship Ship { get; set; }

    private Dictionary<Vector2, Sides> BlockMap { get; set; }

    public GridHandler(Ship ship)
    {
        Ship = ship;
        BlockMap = new Dictionary<Vector2, Sides>();
    }

    public void AddBlock(Vector2 pos, Sides blockSides)
    {
        BlockMap[pos] = blockSides;
    }

    public Vector2 FindNearestOpenBlock(Vector2 globalPosition)
    {
        // Find the nearest block to the global position first
        var local = Ship.ToLocal(globalPosition);
        KeyValuePair<Vector2, Sides>? nearest = null;
        var nearestDist = float.MaxValue;
        foreach (var block in BlockMap)
        {
            var localPos = block.Key * 32.0F;
            var dist = localPos.DistanceSquaredTo(local);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = block;
            }
        }
        // For all sides of the nearest, figure out which is closest
        var side = nearest.Value.Value;
        var center = nearest.Value.Key;

        Vector2? nearestSide = null;
        var nearestSideDist = float.MaxValue;
		if ((side & Sides.Top) > 0)
		{
            var pos = center + Vector2.Up;
            if (!BlockMap.ContainsKey(pos))
            {
                var dist = pos.DistanceSquaredTo(local);
                if (dist < nearestSideDist)
                {
                    nearestSideDist = dist;
                    nearestSide = pos;
                }
            }
		}
		if ((side & Sides.Bottom) > 0)
		{
            var pos = center + Vector2.Down;
            if (!BlockMap.ContainsKey(pos))
            {
                var dist = pos.DistanceSquaredTo(local);
                if (dist < nearestSideDist)
                {
                    nearestSideDist = dist;
                    nearestSide = pos;
                }
            }
		}
		if ((side & Sides.Left) > 0)
		{
            var pos = center + Vector2.Left;
            if (!BlockMap.ContainsKey(pos))
            {
                var dist = pos.DistanceSquaredTo(local);
                if (dist < nearestSideDist)
                {
                    nearestSideDist = dist;
                    nearestSide = pos;
                }
            }
		}
		if ((side & Sides.Right) > 0)
		{
            var pos = center + Vector2.Right;
            if (!BlockMap.ContainsKey(pos))
            {
                var dist = pos.DistanceSquaredTo(local);
                if (dist < nearestSideDist)
                {
                    nearestSideDist = dist;
                    nearestSide = pos;
                }
            }
		}
        return nearestSide.Value;
        // Find the nearest side of the nearest block

        // // Build a map of available positions
        // var map = new List<Vector2>();
        // foreach (var pair in BlockMap)
        // {
        //     var position = pair.Key;
        //     var sides = pair.Value;
        //     if ((sides & Sides.Top) > 0)
        //     {
        //         var newPos = position + Vector2.Up;
        //         if (!map.Contains(newPos) && !BlockMap.Keys.Any(x => x == newPos))
        //         {
        //             map.Add(newPos);
        //         }
        //     }
        //     if ((sides & Sides.Bottom) > 0)
        //     {
        //         var newPos = position + Vector2.Down;
        //         if (!map.Contains(newPos) && !BlockMap.Keys.Any(x => x == newPos))
        //         {
        //             map.Add(newPos);
        //         }
        //     }
        //     if ((sides & Sides.Left) > 0)
        //     {
        //         var newPos = position + Vector2.Left;
        //         if (!map.Contains(newPos) && !BlockMap.Keys.Any(x => x == newPos))
        //         {
        //             map.Add(newPos);
        //         }
        //     }
        //     if ((sides & Sides.Right) > 0)
        //     {
        //         var newPos = position + Vector2.Right;
        //         if (!map.Contains(newPos) && !BlockMap.Keys.Any(x => x == newPos))
        //         {
        //             map.Add(newPos);
        //         }
        //     }
        // }
        // var localPosition = Ship.ToLocal(globalPosition);
        // var nearestDist = float.MaxValue;
        // Vector2? nearest = null;
        // foreach (var pos in map)
        // {
        //     var finalPos = pos * 32.0F;
        //     var dist = pos.DistanceSquaredTo(localPosition);
        //     if (nearestDist > dist)
        //     {
        //         nearest = pos;
        //         nearestDist = dist;
        //     }
        // }
        // return nearest.Value;
    }
}