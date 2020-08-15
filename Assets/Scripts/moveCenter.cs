using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントアタッチ時に
// 自動的に Rigidbody2D もアタッチしてくれる
[RequireComponent(typeof(Rigidbody2D))]
public class moveCenter : MonoBehaviour
{
	//中心のオブジェクト
	[SerializeField] GameObject gObject;

    // 円運動周期(1なら1秒かけて1周、2なら2秒かけて一周）
	[SerializeField] float interval = 1;

	//半径
	public float radius = 0.8f;

	[SerializeField] Rigidbody2D rb;

    void Start()
    {
        gObject = GameObject.Find("Enemy5");
    }


    // コンポーネントをアタッチした時とか、
    // コンポーネント右上のメニューからResetを選んだ時に
    // 実行されるメソッド。忘れそうな参照の設定などしておくと便利。
    void Reset()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
	}
    
    //カウントアップ
	private float timer = 0.0f;

	// 現在の回転した回数
	private int currentCount = 0;
    
    void Update()
	{
		//時間をカウントする
		timer += Time.deltaTime;
        
		// timer が interval分 増えるたびに count をインクリメント
		if ((timer / interval) > currentCount + 1)
		{
			++currentCount;
			Debug.Log($"count: {currentCount}");
		}
        
        // countの値によって回転運動を制御する
		if (currentCount >= 2)
		{
			return;
		}
        
		// 現在の回転角（ラジアン）
		// interval の間隔で1周する
		var radianAngle = (timer / interval) * 2 * Mathf.PI;
        
		Vector3 offset = new Vector3(radius * Mathf.Cos(radianAngle), radius * Mathf.Sin(radianAngle));
		rb.MovePosition(gObject.transform.position + offset);
		
		// 向き変えるならこんな感じ
		// SetRotation() の場合は度数法であることに注意
		rb.SetRotation(radianAngle * Mathf.Rad2Deg);
	}




    /*
    private GameObject centerObj;
    //半径
    public float distance;

    private void Start()
    {
        centerObj = GameObject.Find("Enemy5");

        for (int i = 0; i < 360 * Time.deltaTime; i++)
        {
            this.transform.position = centerObj.transform.position + (Quaternion.Euler(0f, 0f, (360f / Time.deltaTime) * i) * Vector3.up * distance);
        }
    }
    */



    /*
/////////////Quaternion.AngleAxis/////////////
private GameObject centerObj;

// 中心点
private Vector3 _center;

// 回転軸
private Vector3 _axis = Vector3.forward;

// 円運動周期(1なら1秒かけて1周、2なら2秒かけて一周）
private float _period = 1;

// 向きを更新するかどうか
private bool _updateRotation = true;

//カウントアップ
private float countup = 0.0f;

private void Start()
{
    centerObj = GameObject.Find("Enemy5");
}
private void Update()
{
    //時間をカウントする
    countup += Time.deltaTime;

    if (countup >= 2)
    {
        return;
    }

    _center = centerObj.transform.position;



    //中心とのベクトルを取る
    var heading = _center - this.transform.position;
    //中心との距離（半径）をとる
    var distance = heading.magnitude;


    // 回転のクォータニオン作成
    var angleAxis = Quaternion.AngleAxis(360 / _period * Time.deltaTime, _axis);


    // 円運動の位置計算

    //現在の位置から、中心位置を引く（ベクトルをとってる）
    var pos = this.transform.position - _center;
    //角度かけるベクトル（現在位置から回転するのかな？）
    pos = angleAxis * pos;
    //中心の位置を足す（なんで？？）
    pos = pos + _center;

    this.transform.position = pos;

    // 向き更新
    if (_updateRotation)
    {
        this.transform.rotation = this.transform.rotation * angleAxis;
    }


}
*/

    /*
    /////////////RotateAround/////////////
    //中心のオブジェクト
    private GameObject gObject;
    //回転のスピード
    private float speed;

    //カウントアップ
    private float countup = 0.0f;

    void Start()
    {
        speed = 2.0f;
        gObject = GameObject.Find("Enemy5");
    }
    void Update()
    {
        //時間をカウントする
        countup += Time.deltaTime;

        if (countup >= 2)
        {
            return;
        }
        //1秒を360で割ったみたいな感じ?よくわからない
        transform.RotateAround(gObject.transform.position, Vector3.forward, 360 * Time.deltaTime);
    }
    */

}
