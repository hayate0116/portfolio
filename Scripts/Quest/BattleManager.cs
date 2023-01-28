using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//�ΐ�Ǘ�
public class BattleManager : MonoBehaviour
{
    public Transform playerDamagePanel;
    public QuestManager questManager; 
    public PlayerUIManager playerUI;
    public EnemyUIManager enemyUI;
    public PlayerManager player;
    EnemyManager enemy;

    bool canAttackPlayer = true;

    public void Start()
    {
        enemyUI.gameObject.SetActive(false);
        //StartCoroutine(SampleCol());
        playerUI.SetupUI(player);
    }

    /*sample�R���[�`��
    IEnumerator SampleCol()
    {
        UnityEngine.Debug.Log("�R���[�`���J�n");
        yield return new WaitForSeconds(2f);
        UnityEngine.Debug.Log("2s�o��");
    }*/

    //�����ݒ�
    public void Setup(EnemyManager enemyManager)
    {
        SoundManager.Instance.PlayBGM("Battle");
        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        enemyUI.SetupUI(enemy);
        playerUI.SetupUI(player);

        enemy.AddEventListenerOnTap(PlayerAttack);

      //enemy.transform.DOMove(new Vector3(10, 0, 0), 5f);
    }

    void PlayerAttack()
    {
        if (canAttackPlayer)
        {
            canAttackPlayer = false;

            StopAllCoroutines();

            SoundManager.Instance.PlaySE(1);
            int damage = player.Attack(enemy);
            enemyUI.UpdateUI(enemy);
            DialogTextManager.instance.SetScenarios(new string[] {
            "��l���̍U��\n�Ԃׂ���"+damage+"�_���[�W�^�����B" });
        }
        if (enemy.hp <= 0)
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
        
    }
    IEnumerator EnemyTurn()
    {   
            yield return new WaitForSeconds(2f);
            SoundManager.Instance.PlaySE(1);
            //Enemy��Player�ɍU��
            playerDamagePanel.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
            int damage = enemy.Attack(player);
            playerUI.UpdateUI(player);
            DialogTextManager.instance.SetScenarios(new string[] { "�Ԃׂ��̍U��\n��l����" + damage + "�_���[�W���󂯂��B" });
        if(player.hp <= 0)
        {
            yield return new WaitForSeconds(2f);
            DialogTextManager.instance.SetScenarios(new string[] { "��l���͐�ӂ��������B" });
            yield return new WaitForSeconds(2f);
            questManager.PlayerDeath();
        }
        canAttackPlayer= true;
    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(2f);
        DialogTextManager.instance.SetScenarios(new string[] { "�Ԃׂ��͓����Ă������B" });
        yield return new WaitForSeconds(2f);
        enemyUI.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
        SoundManager.Instance.PlayBGM("Quest");
        questManager.EndBattle();
        canAttackPlayer= true;
    }
}
