using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrage_1 : MonoBehaviour
{
    /// <summary>
    /// 円卓状にPrefabを生成する
    /// </summary>
    /// <param name="prefab">生成するPrefab</param>
    /// <param name="count">生成数</param>
    /// <param name="center">中心点のオブジェクト</param>
    /// <param name="distance">距離</param>
    /// <param name="isLookAtCenter">中心点の方向に向けるか</param>
    /// 
    private const float speed = 1.0f;

    /*
    private GameObject centerGameObject;
    private Transform centerTransform;
    private Vector3 noMoveCenter;
    private bool isCenter = false;
    */
    public IEnumerator Generate(GameObject prefab, int count, GameObject center, float distance, float distance2, bool isLookAtCenter = true)
    {
        List<GameObject> bullets1 = new List<GameObject>();
        List<GameObject> bullets2 = new List<GameObject>();

        Vector3 uP = new Vector3(0, 1, 0);
        // 敵(自分)の位置を設定
        //位置情報取得じゃないと、オブジェクトもトランスフォームも更新されつづける（同期してるのだと思う）
        Vector3 centerTP = center.transform.position;

        for (int i = 0; i < count; i++)
        {
            //var position = center.transform.position + (Quaternion.Euler(0f, 0f, (360f / count) * i) * center.transform.up * distance);
            var position = centerTP + (Quaternion.Euler(0f, 0f, (360f / count) * i) * uP * distance);
            var obj = Instantiate(prefab, position, Quaternion.identity);
            obj.tag = "BarrageBullet1";
            bullets1.Add(obj);

            if (isLookAtCenter)
            {
                //Vector3 diff = center.transform.position - obj.transform.position;
                Vector3 diff = centerTP - obj.transform.position;                
                obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, diff));

                Debug.Log("obj.transform.rotation①" + obj.transform.rotation);

            }

            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < count; i++)
        {
            //(360f / count * i) に値を加算することで、開始位置をずらせる
            var position = centerTP + (Quaternion.Euler(0f, 0f, 360f / count * i + ((360f / count) / 2)) * uP * distance2);
            var obj = Instantiate(prefab, position, Quaternion.identity);
            obj.tag = "BarrageBullet2";
            bullets2.Add(obj);

            if (isLookAtCenter)
            {
                //弾の位置と、敵の位置の差分をとる
                Vector3 diff = centerTP - obj.transform.position;
                //第一引数の方向を、第二引数の方向へ向ける　上方向を向くベクトル(0, 1, 0)を、敵の方向に向けるということ
                //obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);
                obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Vector2.SignedAngle(Vector2.up, diff));
            }

            
            yield return new WaitForSeconds(0.05f);
        }

        // GameObject型の配列bulletsに、"BarrageBullet1"タグのついたオブジェクトをすべて格納
        //GameObject[] bullets1 = GameObject.FindGameObjectsWithTag("BarrageBullet1");
        //GameObject[] bullets2 = GameObject.FindGameObjectsWithTag("BarrageBullet2");

        //弾を回転させる
        foreach (GameObject bullet in bullets1)
        {
            // z軸を軸にして30度回転させるQuaternionを作成（変数をrotとする）
            Quaternion rot = Quaternion.AngleAxis(60, Vector3.forward);
            // 現在の自信の回転の情報を取得する。
            Quaternion q = bullet.transform.rotation;
            // 合成して、自身に設定
            bullet.transform.rotation = q * rot;
        }
        foreach (GameObject bullet in bullets2)
        {
            // z軸を軸にして30度回転させるQuaternionを作成（変数をrotとする）
            Quaternion rot = Quaternion.AngleAxis(-60, Vector3.forward);
            // 現在の自信の回転の情報を取得する。
            Quaternion q = bullet.transform.rotation;
            // 合成して、自身に設定
            bullet.transform.rotation = q * rot;
        }

        yield return new WaitForSeconds(0.5f);


        //弾を移動させる
        // GameObject型の変数bulletsに、bulletsの中身を順番に取り出す。
        // foreachは配列の要素の数だけループします。
        foreach (GameObject bullet in bullets1)
        {
            //このif文がないと何故か削除されたオブジェクトにアクセスしてエラーになる
            if (bullet != null)
            {
                var angles = bullet.transform.eulerAngles;
                angles.z += 180;
                var direction = Quaternion.Euler(angles) * Vector3.up;

                bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }
        foreach (GameObject bullet in bullets2)
        {
            if (bullet != null)
            {
                var angles = bullet.transform.eulerAngles;
                angles.z += 180;
                var direction = Quaternion.Euler(angles) * Vector3.up;

                bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }

        yield break;
    }

}
