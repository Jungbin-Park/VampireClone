using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class UIManager
{
    UI_Base sceneUI;
    Stack<UI_Base> uiStack = new Stack<UI_Base>();

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

    public void RefreshTimeScale()
    {
        if (uiStack.Count > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
