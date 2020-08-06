using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    // Waveプレハブを格納する
    public GameObject[] waves;

    // 現在のWave
    private int currentWave;

    // Managerコンポーネント
    private Manager manager;

    IEnumerator Start()
    {

        // Waveが存在しなければコルーチンを終了する
        if (waves.Length == 0)
        {
            yield break;
        }

        // Managerコンポーネントをシーン内から探して取得する
        manager = FindObjectOfType<Manager>();



        // 3秒待つ
        yield return new WaitForSeconds(3f);


        //現在のWaveが、Waveの最大数になるまで繰り返す
        for(int i =0; i < waves.Length; i++)
        {
            // Waveを作成する
            GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
            //wave[+1]へ　※要素番号が+1されるのであって、Wave1→2→3になるわけではない
            currentWave++;

            // WaveをEmitterの子要素にする
            wave.transform.parent = transform;

            // 5秒待つ
            yield return new WaitForSeconds(5f);
        }

        //全ての配列を消化し終えたら呼び出す
        manager.phaseNum += 1;
        //Debug.Log(manager.phaseNum);



        /////////スクリプト最適化の記録↓↓//////////
        /*
        // Waveを作成する
        //GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
        GameObject wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
        currentWave++;

        // WaveをEmitterの子要素にする
        wave.transform.parent = transform;


        for(int i = 0; i < 5; i++)
        {
            if(currentWave == 0)
            {
                // 5秒待つ
                yield return new WaitForSeconds(5f);
                wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
                // WaveをEmitterの子要素にする
                wave.transform.parent = transform;

                currentWave = 1;                
            }
            else if(currentWave == 1)
            {
                // 5秒待つ
                yield return new WaitForSeconds(5f);
                wave = (GameObject)Instantiate(waves[currentWave], transform.position, Quaternion.identity);
                // WaveをEmitterの子要素にする
                wave.transform.parent = transform;

                currentWave = 0;
            }
            Debug.Log(i);

        }
        /*


        /*
        // Waveの子要素のEnemyが全て削除されるまで待機する
        while (wave.transform.childCount != 0)
        {
            yield return new WaitForEndOfFrame();
        }

        // Waveの削除
        Destroy(wave);
        */

        /*
        // 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
        if (waves.Length <= ++currentWave)
        {
            currentWave = 1;
        }
        */
    }
}
