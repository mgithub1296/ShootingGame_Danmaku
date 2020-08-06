using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ヒットポイント
    public float hp = 1;

    // スコアのポイント
    public float point = 100;

    // Spaceshipコンポーネント
    Spaceship spaceship;
    // shotScriptコンポーネントを取得
    ShotScript shotscript;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        // shotScriptコンポーネントを取得
        shotscript = GetComponent<ShotScript>();

        // ローカル座標のY軸のマイナス方向に移動する
        //spaceship.Move(transform.up * -1);
        Move(transform.up * -1);

        yield break;
        /*
        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }
        
        while (true)
        {
            /*
            // 子要素を全て取得する
            for (int i = 0; i < transform.childCount; i++)
            {

                Transform shotPosition = transform.GetChild(i);

                // ShotPositionの位置/角度で弾を撃つ
                spaceship.Shot(shotPosition);
            }
            
            shotscript.Shot();

            // shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }*/

    }

    // 機体の移動
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
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
