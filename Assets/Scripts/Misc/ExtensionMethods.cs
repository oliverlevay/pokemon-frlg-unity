using UnityEngine;

public static partial class ExtensionMethods
{
    public static Vector3 Vector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
    public static Vector2 Vector2(this Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                return UnityEngine.Vector2.zero;
            case Direction.Right:
                return UnityEngine.Vector2.right;
            case Direction.Left:
                return UnityEngine.Vector2.left;
            case Direction.Up:
                return UnityEngine.Vector2.up;
            case Direction.Down:
                return UnityEngine.Vector2.down;
            default:
                throw new System.Exception("Unknown direction");
        }
    }
}