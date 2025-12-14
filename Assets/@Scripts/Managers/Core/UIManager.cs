using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class UIManager
{
    int _order = 10;
    int _toastOrder = 500;

    UI_Base sceneUI;
    Stack<UI_Base> uiStack = new Stack<UI_Base>();
    UI_Scene _sceneUI = null;
    public UI_Scene SceneUI { get { return _sceneUI; } }

    public event Action<int> OnTimeScaleChanged;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    internal void SetCanvas(GameObject go, bool sort = true, int sortOrder = 0, bool isToast = false)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        if (canvas == null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
        }

        CanvasScaler cs = go.GetOrAddComponent<CanvasScaler>();
        if (cs != null)
        {
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(1080, 1920);
        }

        go.GetOrAddComponent<GraphicRaycaster>();

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = sortOrder;
        }

        if (isToast)
        {
            _toastOrder++;
            canvas.sortingOrder = _toastOrder;
        }
    }

    public T GetSceneUI<T>() where T : UI_Base
    {
        return sceneUI as T;
    }

    public T ShowSceneUI<T>() where T : UI_Base
    {
        if (sceneUI != null)
            return GetSceneUI<T>();

        string key = typeof(T).Name + ".prefab";
        T ui = Managers.Resource.Instantiate(key, pooling:true).GetOrAddComponent<T>();
        sceneUI = ui;

        return ui;
    }

    public T ShowPopup<T>() where T : UI_Base
    {
        string key = typeof(T).Name + ".prefab";
        T ui = Managers.Resource.Instantiate(key, pooling:true).GetOrAddComponent<T>();
        uiStack.Push(ui);
        RefreshTimeScale();

        return ui;
    }
    public void ClosePopup()
    {
        if (uiStack.Count == 0)
            return;

        UI_Base ui = uiStack.Pop();
        Managers.Resource.Destroy(ui.gameObject);
        RefreshTimeScale();
    }

    public void ClosePopup(UI_Popup popup)
    {
        if (uiStack.Count == 0)
            return;

        if (uiStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        //Managers.Sound.PlayPopupClose();
        ClosePopup();
    }

    public void CloseAllPopupUI()
    {
        while (uiStack.Count > 0)
            ClosePopup();
    }

    // 팝업이 뜨면 게임 시간 일시정지
    public void RefreshTimeScale()
    {
        if (uiStack.Count > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public int GetPopupCount()
    {
        return uiStack.Count;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Time.timeScale = 1;
        _sceneUI = null;
    }
}
