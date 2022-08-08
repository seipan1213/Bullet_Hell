using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharBase
{
	Vector2 move_vec;
	[SerializeField]
	public GameObject bullet;
	[SerializeField]
	public GameObject[] items;

	public Vector3 targetPos;
	public Vector3 targetBasePos;

	[SerializeField]
	private float shotInterval = 1f;
	private float time;
	public GameManager gm;

	[SerializeField]
	private int score;

	[SerializeField]
	private float dropParsent;

	[SerializeField]
	private float speed = 1;
	new void Start()
	{
		base.Start();
		ReTargetPos();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		EnemyMove();
		time += Time.deltaTime;
		if (time > shotInterval)
		{
			Shot();
			time = 0;
		}
	}

	void EnemyMove()
	{
		if (Vector3.Distance(targetPos, this.transform.position) <= 0.2f)
		{
			ReTargetPos();
		}
		Vector3 heading = targetPos - this.transform.position;
		float distance = heading.magnitude;

		move_vec = heading / distance / 100 * speed;
		this.transform.Translate(move_vec);
	}

	void Shot()
	{
		GameObject bl = Instantiate(bullet, this.transform.position, this.transform.rotation);
		bl.GetComponent<Bullet>().shooter = this.gameObject;
	}

	void SpawnItem()
	{
		int item_num = Random.Range(0, this.items.Length);
		Instantiate(this.items[item_num], this.transform.position, this.transform.rotation);
	}

	void ReTargetPos()
	{
		targetPos = new Vector3(targetBasePos.x + Random.Range(-4, 6), targetBasePos.y + Random.Range(-3, 3), 0);
	}

	void OnDestroy()
	{
		gm.DieEnemy(score);
		if (this.health <= 0 && Random.Range(0, 1f) <= dropParsent / 100)
			SpawnItem();
	}
}
