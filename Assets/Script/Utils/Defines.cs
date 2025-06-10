using System.ComponentModel;
using UnityEngine;

public static class Defines
{
    public enum Skill
    {
        Butterfly,
        END
    }

    public enum Boundary 
    {
        [Description("Up")] UP = 0,
        [Description("Down")] DOWN = 1,
        [Description("Left")] LEFT = 2,
        [Description("Right")] RIGHT = 3
    };


    public static readonly Vector2[] Direction = 
        { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };


    public const int Inf = int.MaxValue;
    public const float Infinity = float.MaxValue;
} 