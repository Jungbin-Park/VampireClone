using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 주기적으로 몬스터 스폰
public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기
    float spawnInterval = 0.5f;
    // 몬스터 최대 수
    int maxMonsterCount = 100;
    Coroutine coUpdateSpawningPool;

    public bool Stopped { get; set; } = false;

    void Start()
    {
        coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    IEnumerator CoUpdateSpawningPool()
    {
        while(true)
        {
            TrySpawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void TrySpawn()
    {
        if (Stopped)
            return;

        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= maxMonsterCount)
            return;

        Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 10, 15);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(Define.GOBLIN_ID, Define.SNAKE_ID));
    }
}
