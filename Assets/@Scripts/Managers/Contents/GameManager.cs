using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerController Player { get { return Managers.Object?.Player; } }

    #region 재화

    public int Gold { get; set; }

    int gem = 0;

    public event Action<int> OnGemCountChanged;
    public int Gem 
    { 
        get { return gem; }
        set
        {
            gem = value;
            // GameScene에서 처리
            OnGemCountChanged?.Invoke(value);
        }
    }

    #endregion

    #region 전투

    private int killCount;
    public event Action<int> OnKillCountChanged;

    public int KillCount
    {
        get { return killCount; }
        set 
        { 
            killCount = value; 
            OnKillCountChanged?.Invoke(value); 
        }
    }

    #endregion

    #region 이동

    Vector2 moveDir;

    public event Action<Vector2> OnMoveDirChanged;
    public Vector2 MoveDir
    {
        get { return moveDir; }
        set 
        { 
            moveDir = value;
            OnMoveDirChanged?.Invoke(moveDir);
        }
    }

    #endregion
}
