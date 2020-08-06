using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Enemy_4 : MonoBehaviour
{
    // ヒットポイント
    public float hp = 1;
    // スコアのポイント
    public float point = 100;
    // Spaceshipコンポーネント
    Spaceship spaceship;
    //shotScriptが入る変数
    ShotScript_4 shotScript_4;
    // 弾を撃つ間隔
    public float shotDelay;

    //移動座標1回目
    public Vector3 aP1;
    //移動座標2回目
    public Vector3 aP2;

    //待機時間
    public float startWait;

    int i = 0;

    //弾の種類
    int nm = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(startWait);

        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        AttackSequence();

        // 0.5秒待つ
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator CanShot()
    {
        GameObject Player = GameObject.Find("Player(Clone)");
        while (Player != null)
        {
            //子オブジェクト（ShotDirection）を取得
            GameObject ob = transform.Find("ShotDirection").gameObject;
            //enemyの中にある、shotScriptを取得して変数に格納する
            shotScript_4 = ob.GetComponent<ShotScript_4>();

            nm = i;


            shotScript_4.Execution(nm);
            // shotDelay秒待つ
            yield return new WaitForSeconds(shotDelay);



            if (i < 2)
            {
                i++;
            }
            else if(i == 2)
            {
                // shotDelay秒待つ
                yield return new WaitForSeconds(0.5f);
                i = 0;
            }

        }
    }

    public void AttackSequence()
    {
        //何やら外で宣言するとダメらしい
        Sequence seq = DOTween.Sequence();

        //下に移動※OnCompleteのカッコの位置に注意
        seq.Append(transform.DOMove(aP1, 2f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            StartCoroutine(CanShot());
        }));
        //横に移動
        seq.Append(transform.DOMove(aP2, 6f).SetEase(Ease.Linear));
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
