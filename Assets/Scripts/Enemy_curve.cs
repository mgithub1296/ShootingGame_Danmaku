using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_curve : MonoBehaviour
{  
    // ヒットポイント
    public float hp = 1;

    // スコアのポイント
    public float point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;

    public float speed;

    //Bezierコンポーネント
    Bezier bezier;

    //右の敵のベジェの点
    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;

    //左の敵のベジェの点
    Vector3 pp0;
    Vector3 pp1;
    Vector3 pp2;
    Vector3 pp3;

    //ベジェの進んだ割合
    //プロパティでは初期値を入力する
    public float t;

    //右の敵か左の敵か
    public bool right;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        // Bezierコンポーネントを取得
        bezier = GetComponent<Bezier>();

        if(right == true)
        {
            rightPoint();
        }
        else
        {
            leftPoint();
        }
        /*
        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {

            // 子要素を全て取得する
            for (int i = 0; i < transform.childCount; i++)
            {

                Transform shotPosition = transform.GetChild(i);

                // ShotPositionの位置/角度で弾を撃つ
                spaceship.Shot(shotPosition);
            }

            // shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);            
        }*/
        yield break;
    }

    private void Update()
    {
        //毎フレーム0.003ずつ進んでいく
        t += 0.003f;

        //三次ベジェ曲線を使う
        if (right == true)
        {
            this.transform.position = bezier.GetPoint(p0, p1, p2, p3, t);
        }
        else
        {
            this.transform.position = bezier.GetPoint(pp0, pp1, pp2, pp3, t);
        }
        /*
        if (isSyutoku == false)
        {
            //①開始点に現在地を代入
            p0 = this.transform.position;

            //②中継地点の横座標に、開始地点の横座標を代入
            p1.x = p0.x;
            //③中継地点の縦座標に、開始点-0.1を代入
            p1.y = p0.y - 3f;
            //④終点の横座標に、中継地点の横座標-0.1を代入
            p2.x = p1.x - 3f;
            //⑤終点の縦座標に、中継地点の縦座標を代入
            p2.y = p1.y;


            isSyutoku = true;
        }
        二次ベジェ曲線を使う
        this.transform.position = bezier.GetPoint(p0, p1, p2, t);
        */
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // レイヤー名がBullet (Player)以外の時は何も行わない
        if (layerName != "Bullet(Player)" && layerName != "BulletHoming(Player)") return;

        // PlayerBulletのTransformを取得
        Transform playerBulletTransform = c.transform.parent;

        if (layerName == "Bullet(Player)")
        {          
            // Bulletコンポーネントを取得
            Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

            // ヒットポイントを減らす
            hp = hp - bullet.power;
        }
        else if (layerName == "BulletHoming(Player)")
        {
            // Bulletコンポーネントを取得
            Homing homing = playerBulletTransform.GetComponent<Homing>();

            // ヒットポイントを減らす
            hp = hp - homing.power;
        }

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

    void rightPoint()
    {
        //①開始点に現在地を代入
        p0 = this.transform.position;
        //②中継地点(1)の横座標に、開始地点の横座標を代入
        p1.x = p0.x;
        //③中継地点(1)の縦座標に、開始点-0.1を代入
        p1.y = p0.y - 5f;
        //④中継地点(2)の横座標に、中継地点(1)の横座標-0.1を代入
        p2.x = p1.x - 0.3f;
        //⑤中継地点(2)の横座標に、中継地点(1)の縦座標-0.1を代入
        p2.y = p1.y - 0.3f;
        //⑥終点の横座標に、中継地点(1)の横座標-0.1を代入
        p3.x = p2.x - 5f;
        //⑦終点の縦座標に、中継地点の縦座標(2)を代入
        p3.y = p2.y;
    }

    void leftPoint()
    {
        pp0 = this.transform.position;
        pp1.x = pp0.x;
        pp1.y = pp0.y - 5f;
        pp2.x = pp1.x + 0.3f;
        pp2.y = pp1.y - 0.3f;
        pp3.x = pp2.x + 5f;
        pp3.y = pp2.y;
    }
}