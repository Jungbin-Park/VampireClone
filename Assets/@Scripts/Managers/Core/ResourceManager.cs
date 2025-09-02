using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Object = UnityEngine.Object;

public class ResourceManager
{
    Dictionary<string, Object> resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        if (resources.TryGetValue(key, out Object resource))
            return resource as T;

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling  = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if(prefab == null)
        {
            Debug.Log($"Failed to load prefab : {key}");
            return null;
        }

        // Pooling
        if(pooling)
        {
            return Managers.Pool.Pop(prefab);
        }

        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }
    
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 풀링 오브젝트면 리턴
        if (Managers.Pool.Push(go))
            return;

        Object.Destroy(go);
    }

    #region 어드레서블
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        // 캐시 확인
        if(resources.TryGetValue(key, out Object obj))
        {
            callback?.Invoke(resources as T);
            return;
        }

        // Texture2D로 로딩되는 것을 sprite로 로딩되게 처리
        string loadKey = key;
        if (key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";

        // 리소스 비동기 로딩 시작
        var asyncOperation = Addressables.LoadAssetAsync<T>(key);
        asyncOperation.Completed += (op) =>
        {
            if(op.Result is Texture2D texture)
            {
                // Texture2D를 Sprite로 변환
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                resources.Add(key, sprite);
                callback.Invoke(sprite as T);
                return;
            }

            resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };

    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        // 입력한 라벨에 속한 addressable들의 경로를 반환
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }
    #endregion
}
