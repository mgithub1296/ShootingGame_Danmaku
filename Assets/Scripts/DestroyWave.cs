using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWave : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //子オブジェクトが空の場合、オブジェクトを削除
        if(gameObject.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
