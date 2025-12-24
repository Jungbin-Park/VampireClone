using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성 보장
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다.


    #region Contents

    GameManager game  = new GameManager();
    ObjectManager obj = new ObjectManager();
    PoolManager pool = new PoolManager();
    public static GameManager Game { get { return Instance?.game; } }
    public static ObjectManager Object { get { return Instance?.obj; } }
    public static PoolManager Pool { get {  return Instance?.pool; } }

    #endregion

    #region Core

    DataManager data = new DataManager();
    ResourceManager resource = new ResourceManager();
    SceneManagerEx scene = new SceneManagerEx();
    SoundManager sound = new SoundManager();
    UIManager ui = new UIManager();
    public static DataManager Data { get { return Instance?.data; } }
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static SceneManagerEx Scene { get { return Instance?.scene; } }
    public static SoundManager Sound { get { return Instance?.sound; } }
    public static UIManager UI { get { return Instance?.ui; } }


    #endregion

    

    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject() { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            // TODO : 초기화 코드
            // ex) _instance._game.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
        Object.Clear();
    }
}
