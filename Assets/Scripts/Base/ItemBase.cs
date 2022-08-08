using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
	[SerializeField]
	Vector3 vec;

	void FixedUpdate()
	{
		ItemMove();
	}

	void ItemMove()
	{
		this.transform.Translate(vec / 100);
	}
}
