using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

//StageUIを管理（ステージ数・進行ボタン・街に戻るボタン）

public class StageUIManager : MonoBehaviour
{
    public Text stageText;
    public GameObject NextButton;
    public GameObject ToTownButton;
    public GameObject ClearImage;

    public void Start()
    {
        ClearImage.SetActive(false);
    }

    public void UpdateUI(int CurrentStage)
    {
        stageText.text =string.Format("ステージ：{0}",CurrentStage+1);
    }

    public void HideButtons()
    {
        NextButton.SetActive(false);
        ToTownButton.SetActive(false);
    }
    public void ShowButtons()
    {
        NextButton.SetActive(true);
        ToTownButton.SetActive(true);
    }

    public void ShowClearImage()
    {
        ClearImage.SetActive(true);
        NextButton.SetActive(false);
        ToTownButton.SetActive(true);
    }

}
