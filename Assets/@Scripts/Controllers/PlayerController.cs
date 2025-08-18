using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    }

    void Update()
    {
        //UpdateInput();
        MovePlayer();
    }

    // Device Simulator에서 먹통
    void UpdateInput()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            dir.y += 1;
        if (Input.GetKey(KeyCode.S))
            dir.y -= 1;
        if (Input.GetKey(KeyCode.A))
            dir.x -= 1;
        if (Input.GetKey(KeyCode.D))
            dir.x += 1;

        moveDir = dir.normalized;


    }

    void MovePlayer()
    {
        // temp2
        moveDir = Managers.moveDir;
        
        Vector3 dir = moveDir * speed * Time.deltaTime;
        transform.position += dir;
    }
}
