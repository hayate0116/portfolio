using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    void OnDestroy()
    {
        //TokenMgr�̎Q�Ƃ�����
        Shot.parent = null;
        Enemy.parent = null;
        Bullet.parent = null;
        Particle.parent = null;
        Enemy.target = null;
    }

    enum eState
    {
        Init,//������
        Main,//���C��
        GameClear,//�N���A
        GameOver,//�I�[�o�[
    }
    eState _state = eState.Init;

    void Start()
    {
        Shot.parent = new TokenMgr<Shot>("Shot", 32);//Shot���R�Q�m��
        Particle.parent = new TokenMgr<Particle>("Particle", 256);//Particle��256�m��
        Bullet.parent = new TokenMgr<Bullet>("Bullet", 256);//Bullet��256�m��
        Enemy.parent = new TokenMgr<Enemy>("Enemy", 64);//Bullet��256�m��

        Enemy.target = GameObject.Find("Player").GetComponent<Player>();//�v���C���[�Q�Ƃ�G�ɓo�^
    }

    void Update()
    {
        switch (_state)
        {
            case eState.Init:
                Sound.PlayBgm("bgm");//BGM�Đ�
                _state = eState.Main;//���C���֑J��
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
                DrawLabelCenter("GAME OVER�c");
                break;
        }
    }
}
