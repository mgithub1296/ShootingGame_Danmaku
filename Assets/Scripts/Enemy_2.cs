using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_2 : MonoBehaviour
{
    // ヒットポイント
    public float hp = 1;

    // スコアのポイント
    public float point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;
    // shotScriptコンポーネントを取得
    ShotScript shotscript;

    //自分の現在地を入れる変数
    Vector3 myTransform;

    bool isOne = false;
    bool isTwo = false;
    bool isThree = false;
    bool isKill = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        // shotScriptコンポーネントを取得
        shotscript = GetComponent<ShotScript>();

        // ローカル座標のY軸のマイナス方向に移動する
        //Move(transform.up * -1);

        //Rigidbody2D rb = this.GetComponent<Rigidbody2D>();  // rigidbodyを取得

        AttackSequence();




        yield break;        
    }

    private void Update()
    {
        //これはStart関数に書き込むと座標が変わらなくなってしまうから注意
        myTransform = this.transform.position;

        Debug.Log(myTransform);

        //第一引数：移動後の座標　第二引数：時間
        //transform.DOMoveY(1.7f, 3f).SetEase(Ease.OutQuart);
        
        //transform.DOMove(new Vector3(0, -1.8f, 0), 3f).SetRelative().SetEase(Ease.OutQuart);
        

        //マイナスになるほど進んでいくので、3.5→1.70005　※1.7ぴったりにはならないから、1.75以下になったら判定
        /*
        if (myTransform.y < 1.79f && isOne == false)
        {
            transform.DOMove(new Vector3(1.2f, 0, 0), 2f).SetRelative().SetEase(Ease.OutQuart);
            isOne = true;
        }
        if (myTransform.x > 1.49f && isTwo == false)
        {
            //transform.DOMoveX(0.3f, 2f).SetEase(Ease.OutQuart);
            transform.DOMove(new Vector3(-1.2f, 0, 0), 2f).SetRelative().SetEase(Ease.OutQuart);
            isTwo = true;
        }
        /*
        if (myTransform.x < 0.31f && isTwo == true && isThree == false)
        {
            transform.DOMoveX(1.5f, 2f).SetEase(Ease.OutQuart);
            isThree = true;
        }
        */


        Debug.Log("トランスフォーム" + myTransform.y);
        /*
        Vector2 force = new Vector2(0.0f, -1.0f);    // 力を設定
        rb.AddForce(force);  // 力を加える


        myTransform = this.transform.position;

        //前方に進むと値が小さくなっていくので、1.7より小さくなったら止める
        if (myTransform.y < 1.7f)
        {
            Debug.Log(myTransform.y);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        */        
    }

    public void AttackSequence()
    {

        //何やら外で宣言するとダメらしい

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(1.7f, 1f).SetEase(Ease.OutCubic));

        float oneX = Random.Range(-1.0f, 1.0f);
        float oneY = Random.Range(-0.5f, 0.5f);
        float twoX = Random.Range(-1.0f, 1.0f);
        float twoY = Random.Range(-0.5f, 0.5f);
        float threeX = Random.Range(-5f, 5f);

        //一度目のXが負なら、2度目は正にする
        if(oneX < 0 && twoX < 0)
        {
            twoX *= -1;
        }
        seq.Append(transform.DOMove(new Vector3(oneX, oneY, 0), 1f).SetRelative().SetEase(Ease.OutCubic));
        seq.Append(transform.DOMove(new Vector3(twoX, twoY, 0), 1f).SetRelative().SetEase(Ease.OutCubic));
        seq.Append(transform.DOMove(new Vector3(threeX, -8f, 0), 5f).SetRelative().SetEase(Ease.OutCubic));
    }

        void OnTriggerEnter2D(Collider2D c)
    {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // レイヤー名がBullet (Player)以外の時は何も行わない
        if (layerName != "Bullet(Player)") return;

        // PlayerBulletのTransformを取得
        Transform playerBulletTransform = c.transform.parent;

        // Bulletコンポーネントを取得
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

        // ヒットポイントを減らす
        hp = hp - bullet.power;

        // 弾の削除
        Destroy(c.gameObject);

        // ヒットポイントが0以下であれば
        if (hp <= 0)
        {
            // スコアコンポーネントを取得してポイントを追加
            FindObjectOfType<Score>().AddPoint(point);

            // 爆発
            spaceship.Explosion();

            // エネミーの削除
            Destroy(gameObject);
        }
        else
        {

            spaceship.GetAnimator().SetTrigger("Damage");
        }
    }
}
