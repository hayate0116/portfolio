using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    void Start()
    {
        Sound.LoadBgm("bgm", "魔王魂_bgm01 1");
        Sound.LoadBgm("bgm2", "魔王魂  ファンタジー06");
        Sound.LoadSe("damage", "倒れる");
        Sound.LoadSe("destroy", "爆発2");
    }
}
