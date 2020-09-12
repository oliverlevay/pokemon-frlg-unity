using UnityEngine;

public static partial class ExtensionMethods
{
    public static Vector3 ToVector3(this Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                return new Vector3(0, 0, 0);
            case Direction.Right:
                return new Vector3(1, 0, 0);
            case Direction.Left:
                return new Vector3(-1, 0, 0);
            case Direction.Up:
                return new Vector3(0, 1, 0);
            case Direction.Down:
                return new Vector3(0, -1, 0);
            default:
                throw new System.Exception("Unknown direction");
        }
    }
}