using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Token
{
    public static TokenMgr<Bullet> parent = null;//�G�e�Ǘ�
    //�G�e�擾
    public static Bullet Add(float x, float y, float direction, float speed)
    {
        return parent.Add(x, y, direction, speed);
    }

    void Update()
    {
        if (IsOutside())
        {
            Vanish();
        }
    }
}
