using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Token
{
    public static TokenMgr<Shot> parent = null;//�e
    public static Shot Add(float x, float y, float direction, float speed)
    {
        return parent.Add(x, y, direction, speed);//�C���X�^���X�̎擾
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
        //�p�[�e�B�N������
        Particle p = Particle.Add(X, Y);
        if (p != null)
        {
            p.SetColor(0.1f, 0.1f, 1);//������
            p.MulVelocity(0.7f);//�����x��
        }
        base.Vanish();
    }
}
