using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rigidbody2Dコンポーネントを必須にする
[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour
{
	// 移動スピード
	public float speed;

	// 爆発のPrefab
	public GameObject explosion;

	// アニメーターコンポーネント
	private Animator animator;

	void Start()
	{
		// アニメーターコンポーネントを取得
		animator = GetComponent<Animator>();
	}

	// 爆発の作成
	public void Explosion()
	{
		Instantiate(explosion, transform.position, transform.rotation);
	}

	/*
	// 機体の移動
	public void Move(Vector2 direction)
	{
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
	*/

	// アニメーターコンポーネントの取得
	public Animator GetAnimator()
	{
		return animator;
	}

}
