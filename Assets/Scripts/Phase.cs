using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    // Emitterプレハブを格納する
    public GameObject[] emitters;

    // 現在のEmitter
    private int currentEmitter = 0;

    // Managerコンポーネント
    private Manager manager;

    IEnumerator Start()
    {

        // Phaseが存在しなければコルーチンを終了する
        if (emitters.Length == 0)
        {
            yield break;
        }

        // Managerコンポーネントをシーン内から探して取得する
        manager = FindObjectOfType<Manager>();


        // タイトル表示中は待機
        while (manager.IsPlaying() == false)
        {
            yield return new WaitForEndOfFrame();
        }

        // 3秒待つ
        yield return new WaitForSeconds(3f);
    }

    private void FixedUpdate()
    {
        //Debug.Log("カレントエミッター" + currentEmitter);
        //Debug.Log("フェーズナンバー" + manager.phaseNum);
        //フェーズが終わったら、ここに入る
        if (currentEmitter == manager.phaseNum　&& currentEmitter != emitters.Length)
        {
            //emitterを作成する
            GameObject emitter = (GameObject)Instantiate(emitters[currentEmitter], transform.position, Quaternion.identity);
            //emitters[+1]へ　※要素番号が+1されるのであって、Pave1→2→3になるわけではない
            currentEmitter++;

            // EmitterをPhaseの子要素にする
            emitter.transform.parent = transform;
            Debug.Log("IN");
        }
    }
}
