using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BattlePopup : UI_Popup
{
    #region Enum
    enum GameObjects
    {
        ContentObject,
        //SettingButtonRedDotObject,
        //BattlepassButtonRedDotObject,
        //AccountPassButtonRedDotObject,
        //MissionButtonRedDotObject,
        //AchievementButtonRedDotObject,
        //AttendanceCheckButtonRedDotObject,
        //BagIconImageRedDotObject,
        //EventCenterButtonRedDotObject,
        //OfflineRewardButtonRedDotObject,
        //GameStartCostGroupObject, // 리프레시
        //SurvivalTimeObject, // 리프레시
        //StageRewardProgressFillArea,
        //StageRewardProgressSliderObject,
        //FirstClearRewardUnlockObject,
        //SecondClearRewardUnlockObject,
        //ThirdClearRewardUnlockObject,
        //FirstClearRedDotObject,
        //SecondClearRedDotObject,
        //ThirdClearRedDotObject,
        //FirstClearRewardCompleteObject,
        //SecondClearRewardCompleteObject,
        //ThirdClearRewardCompleteObject,
    }

    enum Buttons
    {
        SettingButton,
        //PaymentRewardButton,
        //AccountPassButton,
        MissionButton,
        AchievementButton,
        AttendanceCheckButton,
        StageSelectButton,
        OfflineRewardButton,
        GameStartButton,

        //FirstClearRewardButton,
        //SecondClearRewardButton,
        //ThirdClearRewardButton,
    }

    enum Texts
    {
        StageNameText,
        //SurvivalWaveText,
        //SurvivalWaveValueText,
        GameStartButtonText,
        //GameStartCostValueText,
        //OfflineRewardText,

        //PaymentRewardTextText,
        //AccountPassText,
        SettingButtonText,
        MissionButtonText,
        AchievementButtonText,
        AttendanceCheckButtonText,

        //FirstClearRewardText,
        //SecondClearRewardText,
        //ThirdClearRewardText,
    }

    enum Images
    {
        StageImage,
        //StageRewardIconImage, // 챕터 보상 상자

        //FirstClearRewardItemImage,
        //SecondClearRewardItemImage,
        //ThirdClearRewardItemImage,
    }

    enum RewardBoxState
    {
        Lock,
        Unlock,
        Complete,
        RedDot
    }
    #endregion


    private void Awake()
    {
        Init();
    }



}
