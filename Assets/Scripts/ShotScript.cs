using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{    
    // 弾のPrefab
    public GameObject bullet;

    // 弾を撃つ間隔
    public float shotDelay;
    
    // 弾を撃つかどうか
    public bool canShot;

 
    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameObject Player = GameObject.Find("Player(Clone)");
        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (canShot == false)
        {
            yield break;
        }

        //Playerが死ぬまで繰り返す
        while (Player != null)
        {

            // 子要素を全て取得する
            for (int i = 0; i < transform.childCount; i++)
            {

                Transform shotPosition = transform.GetChild(i);
                //いれるならここ
                //角度求まる
                float aim = GetAim(this.transform.position, Player.transform.position);
                //最初から真逆向いてるから、逆向いてしまう
                aim += 90;
                //this.transform.Rotate(0.0f, 0.0f, aim);
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aim);
                Shot(shotPosition);
            }

            // shotDelay秒待つ
            yield return new WaitForSeconds(shotDelay);
        }
    }

    // 弾の作成
    public void Shot(Transform origin)
    {
        Instantiate(bullet, origin.position, origin.rotation);
    }

    // p2からp1への角度を求める
    // @param p1 自分の座標
    // @param p2 相手の座標
    // @return 2点の角度(Degree)
    public float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        float degree = rad * Mathf.Rad2Deg;


        return degree;
    }
}