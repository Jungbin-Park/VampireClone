using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerController Player { get { return Managers.Object?.Player; } }

    #region 재화
    public int Gold { get; set; }
    public int Gem { get; set; }
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
