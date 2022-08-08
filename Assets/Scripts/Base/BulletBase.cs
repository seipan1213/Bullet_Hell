using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
	[SerializeField]
	private Vector3 vectorSpeed;
	[SerializeField]
	private float damage;
	private float destroyTime = 5f;
	public GameObject shooter;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		BulletMove();
		destroyTime -= Time.deltaTime;
		if (destroyTime < 0)
			Destroy(this.gameObject);
	}

	void BulletMove()
	{
		this.transform.Translate(vectorSpeed / 100);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (CanHit(other))
		{
			CharBase cb = other.GetComponent<CharBase>();
			if (cb)
				cb.Damage(damage);
			Destroy(this.gameObject);
		}
	}

	bool CanHit(Collider2D other)
	{
		if (!shooter || !other)
		{
			return (true);
		}
		return (shooter != other.gameObject && shooter.tag != other.tag);
	}
}
