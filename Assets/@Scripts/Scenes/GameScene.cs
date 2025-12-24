using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    GameObject snake;
    GameObject slime;
    GameObject goblin;
    GameObject joystick;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        Debug.Log("@>> GameScene Init()");

        base.Init();
        SceneType = Define.Scene.GameScene;

        // TEMP : 모든 에셋 로드 (타이틀 화면에서 로딩해야됨)
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
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

            // 일반/보스 스테이지에 따라 스포닝 풀 활성화/비활성화
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

        foreach(var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"Lvl : {playerData.level}, HP : {playerData.maxHp}");
        }


        // ============
        //   UI 갱신
        // ============
        // 잼 획득 이벤트 구독
        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
        // 몬스터 처치 이벤트 구독
        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanged;
    }

    // TEMP : 젬 먹었을 시 처리
    int collectedGemCount = 0;          // 현재 먹은 잼
    int remainingToTotalGemCount = 10;  // 다음 레벨까지 남은 잼
    public void HandleOnGemCountChanged(int gemCount)
    {
        collectedGemCount++;

        //if (collectedGemCount == remainingToTotalGemCount)
        //{
        //    Managers.UI.ShowPopup<UI_SkillSelectPopup>();
        //    collectedGemCount = 0;
        //    remainingToTotalGemCount *= 2;
        //}

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)collectedGemCount / remainingToTotalGemCount);
    }

    public void HandleOnKillCountChanged(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if(killCount == 50)
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
        {
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
            Managers.Game.OnGemCountChanged -= HandleOnKillCountChanged;
        }
            
    }

    public override void Clear()
    {
        
    }
}
