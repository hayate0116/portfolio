using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Token
{
    //画面外に出ないように、はみ出さないように
    private Vector2 screenBounds;
    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        //オブジェクトが画面外にはみ出ないようにする
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        mainCamera = Camera.main;
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void OnMouseDrag()
    {
        //マウスカーソル及びオブジェクトのスクリーン座標を取得
        Vector3 objectScreenPoint =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        //スクリーン座標をワールド座標に変換
        Vector3 objectWorldPoint = Camera.main.ScreenToWorldPoint(objectScreenPoint);

        //オブジェクトの座標を変更する
        transform.position = objectWorldPoint;
    }

    void LateUpdate()
    {
        //画面外に出ないように
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
        if (Input.GetKey(KeyCode.Space))
        {
            //float px = X + UnityEngine.Random.Range(0, SpriteWidth / 2);
            //float dir = UnityEngine.Random.Range(-3.0f, 3.0f);
            Shot.Add(X, Y, 0, 70);
        }
    }

    //衝突判定
    void OnTriggerEnter2D(Collider2D other)
    {
        string name = LayerMask.LayerToName(other.gameObject.layer);
        switch (name)
        {
            case "Enemy":
            case "Bullet":
                Vanish();//ゲームオーバー
                for (int i = 0; i < 8; i++)
                {
                    Particle.Add(X, Y);
                }
                Sound.PlaySe("damage");
                Sound.StopBgm();
                break;
        }
    }
}
