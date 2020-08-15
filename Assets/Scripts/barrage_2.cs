using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrage_2 : MonoBehaviour
{
    /// <summary>
    /// 円卓状にPrefabを生成する
    /// </summary>
    /// <param name="prefab">生成するPrefab</param>
    /// <param name="count">生成数</param>
    /// <param name="center">中心点のオブジェクト</param>
    /// <param name="distance">距離</param>
    /// <param name="swirlLength">うずまきの長さ</param> 
    /// <param name="swirlDensity">うずまきの密度</param> 
    /// 


    private const float speed = 1.0f;

    public IEnumerator Generate(GameObject prefab, int count, GameObject center, float distance, float swirlLength, float swirlDensity, bool isPlayer)
    {
        //中心点の座標を確保（変数に入れないと（GameObjectだと）中心点が移動し続けてしまう）
        Vector3 centerTP = center.transform.position;

        if(isPlayer == true)
        {

            GameObject Player = GameObject.Find("Player(Clone)");

            for (int i = 0; i < count; i++)
            {
                /*関係ないけど後程検証用のメモ
                Vector3 position = new Vector3(0.0f, 1.0f, 0f);
                //vecをかけないとエラーが出る（かけることでvector2型になる？
                position = Quaternion.Euler(0, 0, 3f * count) * position;
                position.Normalize();

                //何故uPとposition両方かけられないのか？
                position = Quaternion.Euler(0f, 0f, (360f / count) * i) * position * distance;
                */

                //うずまき弾
                var position = centerTP + (Quaternion.Euler(0f, 0f, ((360f / count) + swirlDensity) * i) * Vector3.up * (distance + (i * swirlLength)));
                var obj = Instantiate(prefab, position, Quaternion.identity);

                //弾が出来上がる速さ
                yield return new WaitForSeconds(0.01f);



                
                //角度求まる
                float aim = GetAim(this.transform.position, Player.transform.position);
                aim += -90;
                //弾の角度を取得
                obj.transform.rotation = Quaternion.Euler(0f, 0f, aim + 180);
                //弾のベクトルを取得
                var direction = Quaternion.Euler(0f, 0f, aim) * Vector3.up;

                obj.GetComponent<Rigidbody2D>().velocity = direction * speed;
                
            }
        }

    }

    public IEnumerator GenerateEveryDirection(GameObject prefab, int count, bool isPlayer)
    {
        if (isPlayer == true)
        {

            GameObject Player = GameObject.Find("Player(Clone)");

            for (int i = 0; i < count; i++)
            {
                Vector2 vec = Player.transform.position - this.transform.position;
                vec.Normalize();

                //中心点を維持する必要がないからcenterはいらない
                //Vector3.upがないとベクトルにならない(ホーミングにしない場合はVector3.upをかける)
                vec = Quaternion.Euler(0, 0, (360 / count) * i) * vec;
                var t = Instantiate(prefab, this.transform.position, Quaternion.identity);
                t.GetComponent<Rigidbody2D>().velocity = vec * speed;
            }
        }
        yield break;
    }


    //2点の角度が求まる
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
