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

    //“GŠÇ—
    public static TokenMgr<Enemy> parent = null;
    //“G‚Ì’Ç‰Á
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

    //“G‚ÌID
    int _id = 0;

    //HP
    int _hp = 0;
    //HPæ“¾
    public int HP
    {
        get { return _hp; }
    }

    //ƒ_ƒ[ƒW‚ğ—^‚¦‚é
    bool Damage(int v)
    {
        _hp -= v;
        if (_hp <= 0)
        {
            Vanish();//€–S
            //“|‚µ‚½
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

    //‘_‚¢Œ‚‚¿
    public static Player target = null;
    public float GetAim()
    {
        float dx = target.X - X;
        float dy = target.Y - Y;
        return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    //’e‚ğ”­Ë‚·‚é
    void DoBullet(float direction, float speed)
    {
        Bullet.Add(X, Y, direction, speed);
    }

    public void SetParam(int id)
    {
        if (_id != 0)
        {
            StopCoroutine("_Update" + _id);//ƒRƒ‹[ƒ`ƒ“’â~
        }
        if (id != 0)
        {
            StartCoroutine("_Update" + id);//ƒRƒ‹[ƒ`ƒ“ŠJn
        }
        _id = id;//idİ’è
        //0,1,2,3
        //HPƒe[ƒuƒ‹
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
                //ã‰º‚Å‚Í‚İo‚³‚È‚¢‚æ‚¤‚É
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
        //‰ñ“]‚µ‚È‚ª‚ç‘Å‚Â
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
        //‰ñ“]‚µ‚È‚ª‚ç‘Å‚Â
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
            // 8•b–ˆ‚ÉAŠÔŠu‚U“xA‘¬“x‚P‚Å“GƒLƒƒƒ‰‚ğ’†S‚Æ‚µ‚Ä‘S•ûˆÊ’e”­ËB
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
            // Å‰‚Ì©ƒLƒƒƒ‰Šp“x‚ğ’†S‚Æ‚µ‚Ä{‚R‚O“x`|‚R‚O“x‚Ì”ÍˆÍ‚Å“®‚©‚·
            float dir = baseDirection + Mathf.Sin(count * Mathf.Deg2Rad) * 30.0f;

            // ©ƒLƒƒƒ‰Šp“x‚ğŠO‚µ‚Äã‰º‚R–{‚Ã‚Â‚OD‚O‚T•b–ˆ‚É’e”­Ë
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

    void OnTriggerEnter2D(Collider2D other)//Õ“Ë”»’è
    {
        //Layer–¼‚ğæ“¾
        string name = LayerMask.LayerToName(other.gameObject.layer);
        if (name == "Shot")//Shot‚È‚ç“–‚½‚è
        {
            Shot s = other.GetComponent<Shot>();
            s.Vanish();
            Damage(1);
        }
    }
}
