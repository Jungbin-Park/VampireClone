using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    GameObject snake;
    GameObject slime;
    GameObject goblin;
    GameObject joystick;

    void Start()
    {
        // 모든 에셋 로드
        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if( count == totalCount )
            {
                StartLoaded();
            }
        });
    }

    void StartLoaded()
    {
        GameObject prefab = Managers.Resource.Load<GameObject>("Slime_01.prefab");

        GameObject go = new GameObject() { name = "@Monsters" };
        snake.transform.parent = go.transform;
        //_slime.transform.parent = go.transform;
        goblin.transform.parent = go.transform;


        slime.AddComponent<PlayerController>();
        Camera.main.GetComponent<CameraController>().target = slime;
        joystick.name = "@UI_Joystick";
    }

    void Update()
    {
        
    }
}
