using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{ 
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum Scene
    {
        Unknown,
        TitleScene,
        LobbyScene,
        GameScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
    }

    public enum ObjectType
    {
        Player,
        Monster,
        Projectile,
        Env,
    }

    public enum SkillType
    {
        None = 0,
        EgoSword = 10001,
        FireBall = 10011,
    }

    public enum StageType
    {
        Normal,
        Boss,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead,
    }

    public const int GOBLIN_ID = 202001;
    public const int SNAKE_ID = 202002;
    public const int BOSS_ID = 203000;

    public const int PLAYER_DATA_ID = 201000;
    public const string EXP_GEM_PREFAB = "EXPGem.prefab";

    public const int EGO_SWORD_ID = 10001;
}
