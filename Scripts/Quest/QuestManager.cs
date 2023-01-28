using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections.Specialized;

//クエスト全体を管理
public class QuestManager : MonoBehaviour
{
    public StageUIManager StageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;
    public SceneTranseptionManager sceneTranseptionManager;
    public GameObject questBG;

    //敵に遭遇するテーブル：-1→遭遇しない　0→遭遇
    int[] encountTable = { -1, -1, 0, -1, 0, 0 };

    int CurrentStage = 0;//現在のステージ
    private void Start()
    {
        StageUI.UpdateUI(CurrentStage);
        DialogTextManager.instance.SetScenarios(new string[] { "洞窟についた。" });
    }

    IEnumerator Seaching()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "探索中…" });

        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f).OnComplete(() => questBG.transform.localScale = new Vector3(1, 1, 1));

        SpriteRenderer questBGSpriteRenderer = questBG.GetComponent<SpriteRenderer>();
        questBGSpriteRenderer.DOFade(0, 2f).OnComplete(() => questBGSpriteRenderer.DOFade(1, 0));
        
        yield return new WaitForSeconds(2f);

        CurrentStage++;//進行度増加
        //進行度をUIに反映
        StageUI.UpdateUI(CurrentStage);

        if (encountTable.Length <= CurrentStage)
        {
            UnityEngine.Debug.Log("クエストクリア");
            QuestClear();
            //クリア処理
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
   
    //Nextボタンが押されたら
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
        DialogTextManager.instance.SetScenarios(new string[] { "赤べこが現れた！" });

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
        DialogTextManager.instance.SetScenarios(new string[] { "延べ棒を手に入れた。\n街へ戻ろう。" });

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySE(2);
        //クエストクリアの表示
        //街に戻るボタンのみ表示
        StageUI.ShowClearImage();
        //sceneTranseptionManager.LoadTo("Town");
    }

    public void PlayerDeath()
    {
        sceneTranseptionManager.LoadTo("Town");
    }
}
