using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Token
{
    public static TokenMgr<Particle> parent = null;//パーティクル管理
    public static Particle Add(float x, float y)//インスタンス取得
    {
        Particle p = parent.Add(x, y);
        if (p)
        {
            p.SetVelocity(UnityEngine.Random.Range(0, 359), UnityEngine.Random.Range(2.0f, 4.0f));
            p.SetScale(0.25f, 0.25f);
        }
        return p;
    }

    void Update()
    {
        MulVelocity(0.95f);
        MulScale(0.97f);
        if (Scale < 0.01f)
        {
            Vanish();
        }
    }

}
