using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTranseptionManager : MonoBehaviour
{
    public void LoadTo(string sceneName)
    {
        FadeIOManager.instance.FadeOutToIn(() => Load(sceneName));
    }
    void Load(string sceneName)
    {
        SoundManager.Instance.PlayBGM(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
