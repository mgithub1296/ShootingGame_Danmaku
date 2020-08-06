using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    // 攻撃力
    public float power = 0.25f;

    // 弾の移動スピード
    public int speed = 10;

    // ゲームオブジェクト生成から削除するまでの時間
    public float lifeTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        //1番近い敵をenemyに格納
        GameObject enemy = SerchEnemy("Enemy");
        //Debug.Log(enemy);
        if (enemy != null)
        {
            float speed = 1.0f;
            float step = Time.deltaTime * speed * 50;
            this.transform.position = Vector3.MoveTowards(this.transform.position, enemy.transform.position, step);
            //Debug.Log("test");
        }
        else
        {
            // ローカル座標のY軸方向に移動する
            GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
        }


        // lifeTime秒後に削除
        Destroy(gameObject, lifeTime);
    }

    //1番近い敵を探す関数
    GameObject SerchEnemy(string tagName)
    {
        float dis = 0;           //距離保存用
        float nearDis = 9999f;          //最も近いオブジェクトの距離        
        GameObject targetObj = null; //オブジェクト

        //①タグのついたオブジェクトを全て取得
        //②取得した中で1番プレイヤーに近いオブジェクトを取得
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tagName))
        {
            if (obj != null)
            {
                //Debug.Log("OBJ" + obj);
                //  敵との距離を計算
                dis = Vector3.Distance(obj.transform.position, transform.position);

                //  より近いオブジェクトか、距離が０のオブジェクトなら更新
                if (nearDis > dis || dis == 0)
                {
                    nearDis = dis;          //  距離を保存            
                    targetObj = obj;        //  ターゲットを更新
                }
            }

        }
        //最も近かったオブジェクトを返す
        return targetObj;
    }
}
