using System;
using UnityEngine;
using DG.Tweening;

//�G���Ǘ��i�X�e�[�^�X�E�N���b�N���o�j
public class EnemyManager : MonoBehaviour
{
    //�֐��o�^
    Action tapAction;//�N���b�N���ꂽ�Ƃ��Ɏ��s�������֐��i�O������ݒ肵�����j
    public new string name;
    public int hp;
    public int at;
    public GameObject HitEffect;

    //�U��
    public int Attack(PlayerManager player)
    {
        int at = UnityEngine.Random.Range(1, 16);
        int damage = player.Damage(at);
        return damage;
    }

    //�_���[�W
    public int Damage(int damage)
    {
        Instantiate(HitEffect,this.transform,false);
        transform.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }

    //tapAction�Ɋ֐���o�^����֐������
    public void AddEventListenerOnTap(Action action)
    {
        tapAction += action;
    }

    public void OnTap()
    {
        UnityEngine.Debug.Log("�N���b�N���ꂽ");
        tapAction();
    }
}
