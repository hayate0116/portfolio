using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    //スタートボタンが押されたら
    public void OnTownButton()
    {
        SoundManager.Instance.PlaySE(0);
    }
}
