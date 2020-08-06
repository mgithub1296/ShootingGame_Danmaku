using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_3 : MonoBehaviour
{
    // ヒットポイント
    public float hp = 1;

    // スコアのポイント
    public float point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;

    //特定のオブジェクトのスクリプトを指定するための手続き①
    ShotScript_2 shotScript_2; //shotScriptが入る変数

    // 弾を撃つ間隔
    public float shotDelay;


    // Start is called before the first frame update
    void Start()
    {
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        //enemysの配列の中にある、enemyの中にある、shotScriptを取得して変数に格納する
        //これも元凶だった！2回呼び出すな！
        //shotScript_2 = GetComponent<ShotScript_2>();

    }

    IEnumerator CanShot(GameObject eN)
    {
        GameObject Player = GameObject.Find("Player(Clone)");
        while (Player != null)
        {
            ////↓↓↓これ意味あった！？！？/////これをAttackSequenceから移動させただけで、1番目が動くようになった…何故！？
            //ここでオブジェクトを指定しないと、何故か配列の最後のオブジェクトが参照されてしまう
            Transform eNtransform = eN.transform;
            // 特定のオブジェクトのスクリプトを指定するための手続き②
            //子オブジェクト（ShotDirection）を取得
            GameObject ob = eNtransform.Find("ShotDirection").gameObject;
            //enemysの配列の中にある、enemyの中にある、shotScriptを取得して変数に格納する
            shotScript_2 = ob.GetComponent<ShotScript_2>();
            //////////////////////////////////////




            shotScript_2.Execution();
            Debug.Log("オブジェクトは" + gameObject.name);
            // shotDelay秒待つ
            yield return new WaitForSeconds(shotDelay);
        }
    }

    public void AttackSequence(GameObject eN,Vector3 aP, bool Go)
    {
        if (Go == false)
        {
            StopCoroutine(CanShot(eN));            
        }

        //何やら外で宣言するとダメらしい
        Sequence seq = DOTween.Sequence();

        seq.Append(eN.transform.DOMove(aP, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            if (Go == true)
            {
                StartCoroutine(CanShot(eN));
            }
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
