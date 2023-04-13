using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Token
{
    //��ʊO�ɏo�Ȃ��悤�ɁA�͂ݏo���Ȃ��悤��
    private Vector2 screenBounds;
    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        //�I�u�W�F�N�g����ʊO�ɂ͂ݏo�Ȃ��悤�ɂ���
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        mainCamera = Camera.main;
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void OnMouseDrag()
    {
        //�}�E�X�J�[�\���y�уI�u�W�F�N�g�̃X�N���[�����W���擾
        Vector3 objectScreenPoint =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        //�X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 objectWorldPoint = Camera.main.ScreenToWorldPoint(objectScreenPoint);

        //�I�u�W�F�N�g�̍��W��ύX����
        transform.position = objectWorldPoint;
    }

    void LateUpdate()
    {
        //��ʊO�ɏo�Ȃ��悤��
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

    //�Փ˔���
    void OnTriggerEnter2D(Collider2D other)
    {
        string name = LayerMask.LayerToName(other.gameObject.layer);
        switch (name)
        {
            case "Enemy":
            case "Bullet":
                Vanish();//�Q�[���I�[�o�[
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
