using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    void Start()
    {
        Sound.LoadBgm("bgm", "������_bgm01 1");
        Sound.LoadBgm("bgm2", "������  �t�@���^�W�[06");
        Sound.LoadSe("damage", "�|���");
        Sound.LoadSe("destroy", "����2");
    }
}
