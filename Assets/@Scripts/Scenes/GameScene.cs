using JetBrains.Annotations;
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

    Define.StageType stageType;
    public Define.StageType StageType
    {
        get { return stageType; }
        set
        {
            stageType = value;

            if (spawningPool != null)
            {
                switch (value)
                {
                    case Define.StageType.Normal:
                        spawningPool.Stopped = false;
                        break;
                    case Define.StageType.Boss:
                        spawningPool.Stopped = true;
                        break;
                }
            }
        }
    }

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

        // 잼 획득 이벤트 구독
        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
        // 몬스터 처치 이벤트 구독
        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanged;
    }

    // 젬 먹었을 시 처리
    int collectedGemCount = 0;
    int remainingToTotalGemCount = 10;
    public void HandleOnGemCountChanged(int gemCount)
    {
        collectedGemCount++;

        if (collectedGemCount == remainingToTotalGemCount)
        {
            Managers.UI.ShowPopup<UI_SkillSelectPopup>();
            collectedGemCount = 0;
            remainingToTotalGemCount *= 2;
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)collectedGemCount / remainingToTotalGemCount);
    }

    public void HandleOnKillCountChanged(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if(killCount == 5)
        {
            // Boss
            StageType = Define.StageType.Boss;

            // 일반 몬스터들 전부 삭제
            Managers.Object.DespawnAllMonsters();

            // 보스 몬스터 스폰
            Vector2 spawnPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 10);

            Managers.Object.Spawn<MonsterController>(spawnPos, Define.BOSS_ID);
        }
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
    }
}
