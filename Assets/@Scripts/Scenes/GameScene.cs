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
        GameObject player = Managers.Resource.Instantiate("Slime_01.prefab");
        Utils.GetOrAddComponent<PlayerController>(player);

        var snake = Managers.Resource.Instantiate("Snake_01.prefab");
        var goblin = Managers.Resource.Instantiate("Goblin_01.prefab");
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";
        Camera.main.GetComponent<CameraController>().target = player;
    }

    void Update()
    {
        
    }
}
