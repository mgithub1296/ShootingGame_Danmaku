using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript_4 : MonoBehaviour
{
    // 弾のPrefab
    public GameObject bullet;


    //引数：弾の種類
    public void Execution(int nM)
    {
        GameObject Player = GameObject.Find("Player(Clone)");

        if(nM == 0)
        {
            Transform shotPosition = transform.GetChild(0);
            //いれるならここ
            //角度求まる
            float aim = GetAim(this.transform.position, Player.transform.position);
            //最初から真逆向いてるから、逆向いてしまう
            aim += 90;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aim);
            Shot(shotPosition);
        }
        else if(nM == 1)
        {
            // 子要素を全て取得する
            for (int i = 0; i < 2; i++)
            {

                Transform shotPosition = transform.GetChild(i);
                //いれるならここ
                //角度求まる
                float aim = GetAim(this.transform.position, Player.transform.position);
                //最初から真逆向いてるから、逆向いてしまう
                aim += 90;
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aim);
                Shot(shotPosition);
            }
        }
        else if(nM == 2)
        {
            // 子要素を全て取得する
            for (int i = 0; i < 3; i++)
            {

                Transform shotPosition = transform.GetChild(i);
                //いれるならここ
                //角度求まる
                float aim = GetAim(this.transform.position, Player.transform.position);
                //最初から真逆向いてるから、逆向いてしまう
                aim += 90;
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, aim);
                Shot(shotPosition);
            }
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