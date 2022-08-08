using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBase : MonoBehaviour
{
	[SerializeField]
	protected float maxHealth;
	protected float health;

	protected void Start()
	{
		this.health = this.maxHealth;
	}

	public void Damage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	public void Heal(float healHp)
	{
		this.health += healHp;
		if (this.health > this.maxHealth)
			this.health = this.maxHealth;
	}
}
