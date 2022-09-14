using System;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int _x, int _z)
    {
        this.x = _x;
        this.z = _z;
    }

    public override string ToString()
    {
        return $"(x: {x}, z: {z})";
    }

    // ====== Override
    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }
    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }
}
