using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Token
{
    public static TokenMgr<Shot> parent = null;//親
    public static Shot Add(float x, float y, float direction, float speed)
    {
        return parent.Add(x, y, direction, speed);//インスタンスの取得
    }
    void Update()
    {
        if (IsOutside())
        {
            //DestroyObj();
            Vanish();
        }
    }
    public override void Vanish()
    {
        //パーティクル生成
        Particle p = Particle.Add(X, Y);
        if (p != null)
        {
            p.SetColor(0.1f, 0.1f, 1);//青くする
            p.MulVelocity(0.7f);//少し遅く
        }
        base.Vanish();
    }
}
