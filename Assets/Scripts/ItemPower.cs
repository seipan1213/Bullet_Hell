using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPower : ItemBase
{
	[SerializeField]
	private int powerPoint = 0;
	void OnTriggerEnter2D(Collider2D other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		if (player)
		{
			player.PowerUp(powerPoint);
			Destroy(this.gameObject);
		}
	}
}
