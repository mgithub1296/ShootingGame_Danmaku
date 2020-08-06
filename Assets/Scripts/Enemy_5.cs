using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5 : MonoBehaviour
{
    // ヒットポイント
    public float hp = 1;
    // スコアのポイント
    public float point = 100;
    // Spaceshipコンポーネント
    Spaceship spaceship;
    //barrage_1が入る変数
    barrage_1 barrage_1;
    // 弾を撃つ間隔
    public float shotDelay;

    //移動座標1回目
    public Vector3 aP1;
    //移動座標2回目
    public Vector3 aP2;
    public Vector3 aP3;
    public Vector3 aP4;
    public Vector3 aP5;
    public Vector3 aP6;

    //待機時間
    public float startWait;


    //2つ以上の引数を持つコルーチンを呼び出すための変数
    private IEnumerator m_Coroutine;

    //生成するPrefab
    public GameObject prefab;
    //生成数
    public int count;
    //中心点のオブジェクト
    public GameObject center;
    //距離1
    public float distance;
    //距離2
    public float distance2;
    //中心点の方向に向けるか
    public bool isLookAtCenter = true;

    Sequence seq;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(startWait);

        AttackSequence(1);

        yield return new WaitForSeconds(2f);

        AttackSequence(2);

        yield return new WaitForSeconds(2f);

        AttackSequence(3);

        yield return new WaitForSeconds(2f);

        AttackSequence(4);

        yield return new WaitForSeconds(2f);

        AttackSequence(5);

        yield return new WaitForSeconds(2f);

        AttackSequence(6);

        yield break;
    }
    public void AttackSequence(int num)
    {
        //何やら外で宣言するとダメらしい(出来てる
        seq = DOTween.Sequence();

        //barrage_1を取得して変数に格納する
        barrage_1 = this.gameObject.GetComponent<barrage_1>();

        Vector3 aP = new Vector3(0,0,0);

        if (num == 1)
        {
            aP = aP1;
        }
        else if(num == 2)
        {
            aP = aP2;
        }
        else if (num == 3)
        {
            aP = aP3;
        }
        else if (num == 4)
        {
            aP = aP4;
        }
        else if (num == 5)
        {
            aP = aP5;
        }
        else if (num == 6)
        {
            aP = aP6;
        }

        //下に移動※OnCompleteのカッコの位置に注意
        seq.Append(transform.DOMove(aP, 2f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            // コルーチン開始
            //シーケンス後に呼び出しにしないと、AttackSequence();が実行された後すぐに実行されてしまい、到着前に発動してしまう
            //m_Coroutine = barrage_1.Generate(prefab, count, center, distance, distance2, isLookAtCenter);
            //barrage_1.StartCoroutine(m_Coroutine);
            StartCoroutine(barrage_1.Generate(prefab, count, center, distance, distance2, isLookAtCenter));
        }));
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