using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrage_3 : MonoBehaviour
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


    public IEnumerator Generate(GameObject prefab, int count, GameObject center)
    {        
        Vector3 centerTP = center.transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector2 vec = new Vector2(0.0f, 1.0f);
            vec = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)) * vec;
            vec *= speed;
            var q = Quaternion.Euler(0, 0, (-Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg) + 180);
            var t = Instantiate(prefab, centerTP, q);
            t.GetComponent<Rigidbody2D>().velocity = vec;

        //弾が出来上がる速さ
        yield return new WaitForSeconds(0.001f);
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
