using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public static bool bDestroyed = false;

    bool _bStart = false;//�R���[�`���̊m�F

    void Start()
    {
        SetParam(0);
        bDestroyed = false;
    }

    public override void Vanish()
    {
        bDestroyed = true;
        base.Vanish();
    }

    void Update()
    {
        if (_bStart == false)
        {
            //�G�����J�n
            StartCoroutine("_GenerateEnemy");
            _bStart = true;
        }
    }

    void OnGUI()
    {
        Util.SetFontColor(Color.white);//�F������
        Util.SetFontSize(60);
        Util.SetFontAlignment(TextAnchor.MiddleCenter);//��������

        string text = string.Format("{0,3}", HP);
        Util.GUILabel(800, 30, 120, 30, text);
    }

    void BulletReflect()
    {
        float aim = GetAim();
        AddEnemy(4, aim, 3);
        AddEnemy(4, aim - 30, 3);
        AddEnemy(4, aim + 30, 3);
        AddEnemy(4, aim + 30, 3);
    }

    void Bulletall()
    {
        AddEnemy(5, 45, 3);
        AddEnemy(5, -45, 3);
    }

    IEnumerator _GenerateEnemy()
    {
        while (true)
        { 
            AddEnemy(1, 135, 5);
            AddEnemy(1, 225, 5);
            yield return new WaitForSeconds(3);
            BulletReflect();
            yield return new WaitForSeconds(2);
            AddEnemy(2, 90, 5);
            AddEnemy(2, 270, 5);
            Bulletall();
            yield return new WaitForSeconds(5);
            AddEnemy(3, 45, 5);
            AddEnemy(3, -45, 5);
            yield return new WaitForSeconds(3);
            BulletReflect();
            yield return new WaitForSeconds(2);
            Bulletall();
            yield return new WaitForSeconds(3);
            AddEnemy(6, 100, 5);
            AddEnemy(6, 200, 5);
            yield return new WaitForSeconds(2); 
            AddEnemy(1, 135, 5);
            AddEnemy(1, 225, 5);
            yield return new WaitForSeconds(3);
            Bulletall();
            yield return new WaitForSeconds(5);
            AddEnemy(3, 45, 5);
            AddEnemy(3, -45, 5);
            yield return new WaitForSeconds(3);
            BulletReflect();
            yield return new WaitForSeconds(2);
            Bulletall();
            AddEnemy(1, 135, 5);
            AddEnemy(1, 225, 5);
            yield return new WaitForSeconds(3);
            BulletReflect();
            yield return new WaitForSeconds(2);
            AddEnemy(2, 90, 5);
            AddEnemy(2, 270, 5);
            Bulletall();
            yield return new WaitForSeconds(5);
            AddEnemy(6, 100, 5);
            AddEnemy(6, 200, 5);
        }
    }

    Enemy AddEnemy(int id, float direction, float speed)
    {
        return Enemy.Add(id, X, Y, direction, speed);//�G����
    }
}
