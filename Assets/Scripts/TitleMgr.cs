using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleMgr : MonoBehaviour
{
    bool _bDrawPressStart = false;

    IEnumerator Start()
    {
        while (true)
        {
            _bDrawPressStart = !_bDrawPressStart;
            yield return new WaitForSeconds(0.6f);
        }
    }

    void Update()
    {
        Sound.PlayBgm("bgm2");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
            Sound.StopBgm();
        }
    }

    void OnGUI()
    {
        if (_bDrawPressStart)
        {
            Util.SetFontSize(80);
            Util.SetFontAlignment(TextAnchor.MiddleCenter);
            float w = 128;
            float h = 28;
            float px = Screen.width / 2 - w / 2;
            float py = Screen.height / 2 - h / 2;
            py += 65;
            Util.GUILabel(px, py, w, h, "スペースキーでゲーム開始");
        }
    }
}
