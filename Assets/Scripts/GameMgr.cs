using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    void OnDestroy()
    {
        //TokenMgrの参照を消す
        Shot.parent = null;
        Enemy.parent = null;
        Bullet.parent = null;
        Particle.parent = null;
        Enemy.target = null;
    }

    enum eState
    {
        Init,//初期化
        Main,//メイン
        GameClear,//クリア
        GameOver,//オーバー
    }
    eState _state = eState.Init;

    void Start()
    {
        Shot.parent = new TokenMgr<Shot>("Shot", 32);//Shotを３２個確保
        Particle.parent = new TokenMgr<Particle>("Particle", 256);//Particleを256個確保
        Bullet.parent = new TokenMgr<Bullet>("Bullet", 256);//Bulletを256個確保
        Enemy.parent = new TokenMgr<Enemy>("Enemy", 64);//Bulletを256個確保

        Enemy.target = GameObject.Find("Player").GetComponent<Player>();//プレイヤー参照を敵に登録
    }

    void Update()
    {
        switch (_state)
        {
            case eState.Init:
                Sound.PlayBgm("bgm");//BGM再生
                _state = eState.Main;//メインへ遷移
                break;
            case eState.Main:
                if (Boss.bDestroyed)
                {
                    Sound.StopBgm();
                    _state = eState.GameClear;
                }
                else if (Enemy.target.Exists == false)
                {
                    _state = eState.GameOver;
                }
                break;
            case eState.GameClear:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Title");
                }
                break;
            case eState.GameOver:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Main");
                }
                break;
        }
    }

    void DrawLabelCenter(string message)
    {
        Util.SetFontSize(70);
        Util.SetFontAlignment(TextAnchor.MiddleCenter);
        float w = 128;
        float h = 32;
        float px = Screen.width / 2 - w / 2;
        float py = Screen.height / 2 - h / 2;
        Util.GUILabel(px, py, w, h, message);
    }

    void OnGUI()
    {
        switch (_state)
        {
            case eState.GameClear:
                DrawLabelCenter("GAME CLEAR!");
                break;
            case eState.GameOver:
                DrawLabelCenter("GAME OVER…");
                break;
        }
    }
}
