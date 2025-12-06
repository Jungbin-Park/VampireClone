using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCardItem : UI_Base
{
    // ========================
    // 카드 아이템 눌렀을 때 처리
    // ========================

    // 스킬 정보
    int templateID;
    Data.SkillData skillData;

    public void SetInfo(int _templateID)
    {
        templateID = _templateID;

        Managers.Data.SkillDic.TryGetValue(templateID, out skillData);

    }

    public void OnClickItem()
    {
        // 스킬 레벨 업그레이드
        Debug.Log("OnClickItem");
        Managers.UI.ClosePopup();


    }
}
