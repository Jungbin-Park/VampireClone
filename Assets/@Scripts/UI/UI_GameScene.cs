using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_Base
{
    [SerializeField]
    TextMeshProUGUI killCountText;

    [SerializeField]
    Slider gemSlider;

    public void SetGemCountRatio(float ratio)
    {
        gemSlider.value = ratio;
    }

    public void SetKillCount(int killCount)
    {
        killCountText.text = $"{killCount}";
    }

    //public void SetInfo()
    //{

    //}

    //public void RefreshUI()
    //{

    //}


}
