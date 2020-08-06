using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    private Vector3 latestPos;  //前回のPosition
    Vector3 z = new Vector3(0.0f, 0.0f, 1.0f);

    private void Update()
    {
        Vector3 diff = latestPos - transform.position;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position;  //前回のPositionの更新

        //ベクトルの大きさが0.01以上の時に向きを変える処理をする
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(z,diff); //向きを変更する
        }
    }
}
