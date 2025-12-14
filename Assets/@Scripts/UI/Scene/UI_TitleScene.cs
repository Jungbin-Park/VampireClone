using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitleScene : UI_Scene
{
    #region enum
    // 들고 있는 UI 오브젝트들 ENUM
    enum GameObjects
    {
        Slider,
    }

    enum Buttons
    {
        StartButton,
    }

    enum Texts
    {
        StartText,
    }
    #endregion

    bool isPreload = false;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        // 오브젝트 바인딩
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.Slider).GetComponent<Slider>().value = 0;

        // 테스트
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(() =>
        {
            if (isPreload)
                Managers.Scene.LoadScene(Define.Scene.LobbyScene, transform);
        });
        GetButton((int)Buttons.StartButton).gameObject.SetActive(false);
        return true;
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            GetObject((int)GameObjects.Slider).GetComponent<Slider>().value = (float)count / totalCount;

            if (count == totalCount)
            {
                isPreload = true;
                GetButton((int)Buttons.StartButton).gameObject.SetActive(true);
                Managers.Data.Init();
                Managers.Game.Init();
                Managers.Time.Init();
                StartButtonAnimation();
            }
        });
    }

    void StartButtonAnimation()
    {
        //GetText((int)Texts.StartText).DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic).Play();
    }
}
