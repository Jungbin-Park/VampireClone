using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class UI_LobbyScene : UI_Scene
{


    #region enum
    // 들고 있는 UI 오브젝트들 ENUM
    enum GameObjects
    {
        MenuToggleGroup,
        CheckShopImageObject,
        CheckEquipmentImageObject,
        CheckBattleImageObject,
    }

    enum Buttons
    {

    }

    enum Texts
    {
        ShopToggleText,
        EquipmentToggleText,
        BattleToggleText,
        StaminaValueText,
        DiaValueText,
        GoldValueText
    }

    enum Toggles
    {
        ShopToggle,
        EquipmentToggle,
        BattleToggle,
    }

    enum Images
    {
        Backgroundimage,
    }

    #endregion region


    UI_BattlePopup battlePopupUI;
    bool isSelectedBattle = false;


    public override bool Init()
    {
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindToggle(typeof(Toggles));
        BindImage(typeof(Images));

        GetToggle((int)Toggles.BattleToggle).gameObject.BindEvent(OnClickBattleToggle);


        return true;
    }

    void Refresh()
    {
        //GetText((int)Texts.StaminaValueText).text = $"{Managers.Game.Stamina}/{Define.MAX_STAMINA}";
        //GetText((int)Texts.DiaValueText).text = Managers.Game.Dia.ToString();
        GetText((int)Texts.GoldValueText).text = Managers.Game.Gold.ToString();

        // 토글 선택 시 리프레시 버그 대응
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetObject((int)GameObjects.MenuToggleGroup).GetComponent<RectTransform>());
    }


    // 토글 초기화
    void TogglesInit()
    {
        #region 팝업 초기화

        battlePopupUI.gameObject.SetActive(false);

        #endregion

        #region 토글 버튼 초기화
        // 재 클릭 방지 트리거 초기화
        isSelectedBattle = false;
        //GetToggle((int)Toggles.ChallengeToggle).enabled = false; // 도전 탭 비활성화 #Neo
        //GetToggle((int)Toggles.EvolveToggle).enabled = false; // 진화 탭 비활성화 #Neo

        // 버튼 레드닷 초기화
        //GetObject((int)GameObjects.ShopToggleRedDotObject).SetActive(false);
        //GetObject((int)GameObjects.EquipmentToggleRedDotObject).SetActive(false);
        //GetObject((int)GameObjects.BattleToggleRedDotObject).SetActive(false);
        //GetObject((int)GameObjects.ChallengeToggleRedDotObject).SetActive(false);
        //GetObject((int)GameObjects.EvolveToggleRedDotObject).SetActive(false);

        // 선택 토글 아이콘 초기화
        GetObject((int)GameObjects.CheckShopImageObject).SetActive(false);
        GetObject((int)GameObjects.CheckEquipmentImageObject).SetActive(false);
        GetObject((int)GameObjects.CheckBattleImageObject).SetActive(false);
        //GetObject((int)GameObjects.CheckChallengeImageObject).SetActive(false);
        //GetObject((int)GameObjects.CheckEvolveImageObject).SetActive(false);

        GetObject((int)GameObjects.CheckShopImageObject).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 155);
        GetObject((int)GameObjects.CheckEquipmentImageObject).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 155);
        GetObject((int)GameObjects.CheckBattleImageObject).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 155);
        //GetObject((int)GameObjects.CheckChallengeImageObject).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 155);
        //GetObject((int)GameObjects.CheckEvolveImageObject).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 155);


        // 메뉴 텍스트 초기화
        GetText((int)Texts.ShopToggleText).gameObject.SetActive(false);
        GetText((int)Texts.EquipmentToggleText).gameObject.SetActive(false);
        GetText((int)Texts.BattleToggleText).gameObject.SetActive(false);
        //GetText((int)Texts.ChallengeToggleText).gameObject.SetActive(false);
        //GetText((int)Texts.EvolveToggleText).gameObject.SetActive(false);

        // 토글 크기 초기화
        GetToggle((int)Toggles.ShopToggle).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);
        GetToggle((int)Toggles.EquipmentToggle).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);
        GetToggle((int)Toggles.BattleToggle).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);
        //GetToggle((int)Toggles.ChallengeToggle).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);
        //GetToggle((int)Toggles.EvolveToggle).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 150);

        #endregion

    }

    void ShowUI(GameObject contentPopup, Toggle toggle, TMP_Text text, GameObject obj2, float duration = 0.1f)
    {
        TogglesInit();

        contentPopup.SetActive(true);
        toggle.GetComponent<RectTransform>().sizeDelta = new Vector2(280, 150);
        text.gameObject.SetActive(true);
        obj2.SetActive(true);
        //obj2.GetComponent<RectTransform>().DOSizeDelta(new Vector2(200, 180), duration).SetEase(Ease.InOutQuad);

        Refresh();
    }

    void OnClickBattleToggle()
    {
        GetImage((int)Images.Backgroundimage).color = Utils.HexToColor("1F5FA0"); // 배경 색상 변경
        if (isSelectedBattle == true) // 활성화 후 토글 클릭 방지
            return;
        ShowUI(battlePopupUI.gameObject, GetToggle((int)Toggles.BattleToggle), GetText((int)Texts.BattleToggleText), GetObject((int)GameObjects.CheckBattleImageObject));
        isSelectedBattle = true;
    }

}
