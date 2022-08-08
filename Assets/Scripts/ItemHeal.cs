using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : ItemBase
{
	[SerializeField]
	private float healHp = 0;
	void OnTriggerEnter2D(Collider2D other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		if (player)
		{
			player.Heal(healHp);
			Destroy(this.gameObject);
		}
	}
}
