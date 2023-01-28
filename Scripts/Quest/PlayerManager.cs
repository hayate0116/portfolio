using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int hp;
    public int at;

    //UŒ‚
    public int Attack(EnemyManager enemy)
    {
        int at = UnityEngine.Random.Range(1, 16);
        int damage = enemy.Damage(at);
        return damage;
    }

    //ƒ_ƒ[ƒW
    public int Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }
}
