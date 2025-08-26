using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 moveDir = Vector2.zero;
    float speed = 5.0f;

    public Vector2 MoveDir
    {
        get { return moveDir; }
        set {  moveDir = value.normalized; }    
    }

    void Start()
    {
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
        {
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        }
    }

    void HandleOnMoveDirChanged(Vector2 dir)
    {
        MoveDir = dir;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //moveDir = Managers.Game.MoveDir;
        Vector3 dir = moveDir * speed * Time.deltaTime;
        transform.position += dir;
    }
}
