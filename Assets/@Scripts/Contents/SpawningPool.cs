using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기
    // 몬스터 최대 수
    float spawnInterval = 1.0f;
    int maxMonsterCount = 100;
    Coroutine coUpdateSpawningPool;

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
        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= maxMonsterCount)
            return;

        // temp : dataID
        MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
        mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    }
}
