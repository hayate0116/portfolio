using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Token
{
    public Sprite Spr0;
    public Sprite Spr1;
    public Sprite Spr2;
    public Sprite Spr3;
    public Sprite Spr4;
    public Sprite Spr5;
    public Sprite Spr6;

    //敵管理
    public static TokenMgr<Enemy> parent = null;
    //敵の追加
    public static Enemy Add(int id, float x, float y, float direction, float speed)
    {
        Enemy e = parent.Add(x,y, direction, speed);
        if(e == null)
        {
            return null;
        }
        e.SetParam(id);
        return e;
    }

    //敵のID
    int _id = 0;

    //HP
    int _hp = 0;
    //HP取得
    public int HP
    {
        get { return _hp; }
    }

    //ダメージを与える
    bool Damage(int v)
    {
        _hp -= v;
        if (_hp <= 0)
        {
            Vanish();//死亡
            //倒した
            for (int i = 0; i < 4; i++)
            {
                Particle.Add(X, Y);
            }
            Sound.PlaySe("destroy",0);
            if (_id == 0)
            {
                Enemy.parent.ForEachExist(e => e.Damage(9999));
                if (Bullet.parent != null)
                {
                    Bullet.parent.Vanish();
                }
            }
            return true;
        }
        return false;
    }

    //狙い撃ち
    public static Player target = null;
    public float GetAim()
    {
        float dx = target.X - X;
        float dy = target.Y - Y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    //弾を発射する
    void DoBullet(float direction, float speed)
    {
        Bullet.Add(X, Y, direction, speed);
    }

    public void SetParam(int id)
    {
        if (_id != 0)
        {
            StopCoroutine("_Update" + _id);//コルーチン停止
        }
        if (id != 0)
        {
            StartCoroutine("_Update" + id);//コルーチン開始
        }
        _id = id;//id設定
        //0,1,2,3
        //HPテーブル
        int[] hps = { 6000, 30000, 30000, 30000, 30000, 30000, 30000 };
        Sprite[] sprs = { Spr0, Spr1, Spr2, Spr3, Spr4, Spr5,Spr6 };
        _hp = hps[id];
        SetSprite(sprs[id]);
        Scale = 0.5f;
    }

    void Update()
    {
        if (_id == 4)
        {
            Vector2 min = GetWorldMin();
            Vector2 max = GetWorldMax();

            if (Y < min.y || max.y < Y)
            {
                //上下ではみ出さないように
                ClampScreen();
                VY *= -1;
            }
            if (X < min.x || max.x < X)
            {
                Vanish();
            }
            Scale = 0.2f;

        }
    }

    IEnumerator _Update1()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            float dir = GetAim();
            Bullet.Add(X, Y, dir, 2);
            Scale = 0.01f;
        }
    }
    IEnumerator _Update2()
    {
        //回転しながら打つ
        yield return new WaitForSeconds(2.0f);
        float dir = 0;
        while (true)
        {
            Bullet.Add(X, Y, dir, 2);
            dir += 16;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator _Update3()
    {
        //回転しながら打つ
        yield return new WaitForSeconds(2.0f);
        DoBullet(180 - 12, 2);
        DoBullet(180, 2);
        DoBullet(180 + 12, 2);
       
    }
    IEnumerator _Update4()
    {
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator _Update5()
    {
        float baseDir = GetAim();
        int count = 0;
        while (true)
        {
            // 8秒毎に、間隔６度、速度１で敵キャラを中心として全方位弾発射。
            for (int rad = 0; rad < 360; rad += 6)
            {
                Bullet.Add(transform.position.x, transform.position.y, rad, 1);
            }
            yield return new WaitForSeconds(8.0f);
            count++;
        }
        
    }
    IEnumerator _Update6()
    {
        float baseDirection = GetAim();
        int count = 0;
        while (true)
        {
            // 最初の自キャラ角度を中心として＋３０度〜−３０度の範囲で動かす
            float dir = baseDirection + Mathf.Sin(count * Mathf.Deg2Rad) * 30.0f;

            // 自キャラ角度を外して上下３本づつ０．０５秒毎に弾発射
            for (int index = -3; index < 3; index++)
            {
                Bullet.Add(transform.position.x, transform.position.y, dir + index * 30 + 15, 3);
            }
            yield return new WaitForSeconds(0.35f);
            count++;
        }
    }

    void FixedUpdate()
    {
        if (_id <= 3 || _id >= 5)
        {
            MulVelocity(0.93f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)//衝突判定
    {
        //Layer名を取得
        string name = LayerMask.LayerToName(other.gameObject.layer);
        if (name == "Shot")//Shotなら当たり
        {
            Shot s = other.GetComponent<Shot>();
            s.Vanish();
            Damage(1);
        }
    }
}
