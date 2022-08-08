using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharBase
{
	[SerializeField]
	private float f_speed;
	[SerializeField]
	private float b_speed;
	[SerializeField]
	private float r_speed;
	[SerializeField]
	private float l_speed;

	public GameObject bullet;

	private int powerLevel = 0;

	private Animator animtor;
	private enum eAnimParam
	{
		CENTER,
		RIGHT,
		LEFT,
	}

	new void Start()
	{
		base.Start();
		animtor = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		PlayerMove();
		Shot();
	}

	void PlayerMove()
	{
		if (Input.GetAxis("Vertical") > 0.1f)
			this.transform.Translate(0, f_speed, 0);
		if (Input.GetAxis("Vertical") < -0.1f)
			this.transform.Translate(0, -b_speed, 0);
		if (Input.GetAxis("Horizontal") > 0.1f)
		{
			ChangeAnimParam(eAnimParam.RIGHT);
			this.transform.Translate(r_speed, 0, 0);
		}
		else if (Input.GetAxis("Horizontal") < -0.1f)
		{
			ChangeAnimParam(eAnimParam.LEFT);
			this.transform.Translate(-l_speed, 0, 0);
		}
		else
		{
			ChangeAnimParam(eAnimParam.CENTER);
		}


	}

	void Shot()
	{
		GameObject bl = Instantiate(bullet, this.transform.position, this.transform.rotation);
		bl.GetComponent<Bullet>().shooter = this.gameObject;
	}

	public void PowerUp(int point)
	{
		if (powerLevel >= 100)//TODO: 弾種類数
		{
			Heal(10);
			return;
		}
		powerLevel += point;
	}

	void SetAnimParam(eAnimParam param, bool isTrue)
	{
		switch (param)
		{
			case eAnimParam.CENTER:
				animtor.SetBool("Center", isTrue);
				break;
			case eAnimParam.RIGHT:
				animtor.SetBool("Right", isTrue);
				break;
			case eAnimParam.LEFT:
				animtor.SetBool("Left", isTrue);
				break;
			default:
				break;
		}
	}

	void ChangeAnimParam(eAnimParam param)
	{
		SetAnimParam(eAnimParam.CENTER, false);
		SetAnimParam(eAnimParam.RIGHT, false);
		SetAnimParam(eAnimParam.LEFT, false);

		SetAnimParam(param, true);
	}
}
