using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

//StageUI���Ǘ��i�X�e�[�W���E�i�s�{�^���E�X�ɖ߂�{�^���j

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
        stageText.text =string.Format("�X�e�[�W�F{0}",CurrentStage+1);
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
