using System.ComponentModel;
using UnityEngine;

public static class Defines
{
    public enum Scene
    {
        StartScene,
        GamePlayScene
    }

    public enum Sound
    {
        Bgm = 0,
        Effect
    }

    public enum State
    {
        Chase,
        Appear,
        Follow,
        Wait,
        Move,
        Disappear,
        END
    }

    public enum Skill
    {
        ButterflyExplosion,
        Squirrals,
        Butterfly,
        SpreadBeaver,
        StraightBeaver,
        ShieldBee,
        StraightBee
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Down,
        Up
    }

    public enum Boundary 
    {
        [Description("Up")] UP = 0,
        [Description("Down")] DOWN = 1,
        [Description("Left")] LEFT = 2,
        [Description("Right")] RIGHT = 3
    };

    public static readonly Vector2[] Direction =
        { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    public const int Inf = int.MaxValue;
    public const float Infinity = float.MaxValue;
} 