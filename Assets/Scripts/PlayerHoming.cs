using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoming : MonoBehaviour
{
    // PlayerBullet_Homingプレハブ
    public GameObject bullethoming;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            // 弾をプレイヤーと同じ位置/角度で作成
            Instantiate(bullethoming, transform.position, transform.rotation);
            //spaceship.Shot(transform);

            // ショット音を鳴らす
            GetComponent<AudioSource>().Play();

            // 0.05秒待つ
            yield return new WaitForSeconds(0.5f);
            // shotDelay秒待つ
            //yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
