using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

class Pool
{
    GameObject prefab;
    IObjectPool<GameObject> pool;

    // 풀링 객체들을 묶어서 관리하기 위한 최상위 루트
    Transform root;
    Transform Root
    {
        get
        {
            if(root == null)
            {
                GameObject go = new GameObject() { name = $"{prefab.name} Root" };
                root = go.transform;
            }

            return root;
        }
    }

    public Pool(GameObject _prefab)
    {
        // 복사본 생성
        prefab = _prefab;
        pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public void Push(GameObject go)
    {
        pool.Release(go);
    }

    public GameObject Pop()
    {
        // 오브젝트 풀에서 꺼내옴
        return pool.Get();
    }

    #region Funcs

    GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.parent = Root;
        go.name = prefab.name;
        return go;
    }

    void OnGet(GameObject go)
    {
        go.SetActive(true);
    }

    void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }

    void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }

    #endregion
}

public class PoolManager
{
    Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    public GameObject Pop(GameObject prefab)
    {
        // 키 값에 해당하는 pool이 없으면 생성
        if(pools.ContainsKey(prefab.name) == false)
            CreatePool(prefab);

        // 프리팹 이름을 키 값으로 사용
        return pools[prefab.name].Pop();
    }

    public bool Push(GameObject go)
    {
        if (pools.ContainsKey(go.name) == false)
            return false;

        pools[go.name].Push(go);
        return true;
    }

    void CreatePool(GameObject prefab)
    {
        Pool pool = new Pool(prefab);
        pools.Add(prefab.name, pool);
    }
}
