using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnerater_Wave3 : MonoBehaviour
{
    //cornPrefabを入れる
    public GameObject Enemy2Prefab;

    int i = 0;

    float j;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (; i < 12; i++)
        {
            GameObject enemy2 = Instantiate(Enemy2Prefab) as GameObject;
            Debug.Log(i);
            
            switch (i)
            {
                case 0:
                case 6:
                case 12:
                    j = 0.3f;
                    break;
                case 1:
                case 7:
                case 13:
                    j = -0.3f;
                    break;
                case 2:
                case 8:
                case 14:
                    j = 0.9f;
                    break;
                case 3:
                case 9:
                case 15:
                    j = -0.9f;
                    break;
                case 4:
                case 10:
                case 16:
                    j = 1.5f;
                    break;
                case 5:
                case 11:
                case 17:
                    j = -1.5f;
                    break;                
            }
            Debug.Log(this.transform.position.y);
            enemy2.transform.position = new Vector2(j, this.transform.position.y);
            
            // 1.5秒待つ
            yield return new WaitForSeconds(0.8f);
            
        }

        yield break;
    }

}
