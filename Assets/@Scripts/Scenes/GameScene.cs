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
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if( count == totalCount )
            {
                StartLoaded();
            }
        });
    }


    //void StartLoaded()
    //{
    //    GameObject player = Managers.Resource.Instantiate("Slime_01.prefab");
    //    Utils.GetOrAddComponent<PlayerController>(player);

    //    var snake = Managers.Resource.Instantiate("Snake_01.prefab");
    //    var goblin = Managers.Resource.Instantiate("Goblin_01.prefab");
        
    //    var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
    //    joystick.name = "@UI_Joystick";

    //    var map = Managers.Resource.Instantiate("Map.prefab");
    //    map.name = "@Map";
    //    Camera.main.GetComponent<CameraController>().target = player;
    //}

    SpawningPool spawningPool;

    void StartLoaded()
    {
        Managers.Data.Init();

        Managers.UI.ShowSceneUI<UI_GameScene>();

        spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map_01.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().target = player.gameObject;

        // Data Test
        Managers.Data.Init();

        foreach(var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"Lvl : {playerData.level}, HP : {playerData.maxHp}");
        }
    }

    void Update()
    {
        
    }
}
