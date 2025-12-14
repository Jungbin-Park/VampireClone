using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static bool s_init = false;

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

    public static Managers Instance
    {
        get
        {
            if(s_init == false)
            {
                s_init = true;

                GameObject go = GameObject.Find("@Managers");
                if(go == null)
                {
                    go = new GameObject() { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();

                // TODO : 초기화 코드
                // ex) _instance._game.Init();
            }

            return s_instance;
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
