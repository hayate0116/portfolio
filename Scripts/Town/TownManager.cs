using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    private void Start()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "äXÇ…Ç¬Ç¢ÇΩÅB" });
    }
    public void OnToQuestButton()
    {
        SoundManager.Instance.PlaySE(0);
    }
}
