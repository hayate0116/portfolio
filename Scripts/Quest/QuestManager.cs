using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections.Specialized;

//�N�G�X�g�S�̂��Ǘ�
public class QuestManager : MonoBehaviour
{
    public StageUIManager StageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;
    public SceneTranseptionManager sceneTranseptionManager;
    public GameObject questBG;

    //�G�ɑ�������e�[�u���F-1���������Ȃ��@0������
    int[] encountTable = { -1, -1, 0, -1, 0, 0 };

    int CurrentStage = 0;//���݂̃X�e�[�W
    private void Start()
    {
        StageUI.UpdateUI(CurrentStage);
        DialogTextManager.instance.SetScenarios(new string[] { "���A�ɂ����B" });
    }

    IEnumerator Seaching()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "�T�����c" });

        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f).OnComplete(() => questBG.transform.localScale = new Vector3(1, 1, 1));

        SpriteRenderer questBGSpriteRenderer = questBG.GetComponent<SpriteRenderer>();
        questBGSpriteRenderer.DOFade(0, 2f).OnComplete(() => questBGSpriteRenderer.DOFade(1, 0));
        
        yield return new WaitForSeconds(2f);

        CurrentStage++;//�i�s�x����
        //�i�s�x��UI�ɔ��f
        StageUI.UpdateUI(CurrentStage);

        if (encountTable.Length <= CurrentStage)
        {
            UnityEngine.Debug.Log("�N�G�X�g�N���A");
            QuestClear();
            //�N���A����
        }

        else if (encountTable[CurrentStage] == 0)
        {
            EncountEnemy();
        }
        else
        {
            StageUI.ShowButtons();
        }
    }
   
    //Next�{�^���������ꂽ��
    public void OnNextButton()
    {
        SoundManager.Instance.PlaySE(0);
        StageUI.HideButtons();
        StartCoroutine(Seaching());
    }

    public void OnToTownButton()
    {
        SoundManager.Instance.PlaySE(0);
    }

    void EncountEnemy()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "�Ԃׂ������ꂽ�I" });

        StageUI.HideButtons();
        GameObject enemyobj = Instantiate(enemyPrefab);
        EnemyManager enemy = enemyobj.GetComponent<EnemyManager>();
        battleManager.Setup(enemy);
    }

    public void EndBattle()
    {
        StageUI.ShowButtons();
    }
    void QuestClear()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "���ז_����ɓ��ꂽ�B\n�X�֖߂낤�B" });

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySE(2);
        //�N�G�X�g�N���A�̕\��
        //�X�ɖ߂�{�^���̂ݕ\��
        StageUI.ShowClearImage();
        //sceneTranseptionManager.LoadTo("Town");
    }

    public void PlayerDeath()
    {
        sceneTranseptionManager.LoadTo("Town");
    }
}
