using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class commander_3 : MonoBehaviour
{
    //動かす敵を指定
    public GameObject[] enemys;

    // Enemy_3コンポーネント
    private Enemy_3 enemy_3;

    //到達点
    public GameObject[] arrivalPointObj;

    //スタート位置を記憶させる
    Vector3[] startPoint = new Vector3[12];

    //行きか帰りか
    bool go = true;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Enemy_3コンポーネントをシーン内から探して取得する
        enemy_3 = FindObjectOfType<Enemy_3>();
        

        //●この構文めっちゃ便利！！
        for (int i = 0; i < enemys.Length; i++)
        {
            //スタート位置を記憶させる
            startPoint[i] = enemys[i].transform.position;

            Debug.Log("①現在の配列は？" + enemys[i]);
            //引数①動かす敵　引数②到達点を指定して、シーケンスを実行。シーケンスである必要性あるか？
            enemy_3.AttackSequence(enemys[i], arrivalPointObj[i].transform.position, go);
            




            // 0.5秒待つ
            yield return new WaitForSeconds(0.5f);
        }


        //スタート位置に戻らせる
        for (int j = 0; j < enemys.Length; j++)
        {
            go = false;

            //引数①動かす敵　引数②到達点を指定して、シーケンスを実行。シーケンスである必要性あるか？
            enemy_3.AttackSequence(enemys[j], startPoint[j], go);

            // 0.5秒待つ
            yield return new WaitForSeconds(0.5f);
        }
    }
}
