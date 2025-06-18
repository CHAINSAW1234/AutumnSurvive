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
        ButterflyExplosion, // 꽃잎 폭탄
        Squirrals, // 유도탄
        Butterfly, // 벌미사일
        //Beaver, // 콩벌레
        SpreadBullet, // 베이비 버그1
        StraightBullet, // 베이비 버그2
        ShieldBee, //코스모스
        StraightBee, // 코스모스2
        //Dummy // 더미
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