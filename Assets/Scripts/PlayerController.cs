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

	private int powerLevel = 1;

	private Animator animtor;

	public GameManager gm;

	[SerializeField]
	private int powerMax = 10;
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
		for (int i = 1; i <= powerLevel; i++)
		{
			Vector3 pos = new Vector3(this.transform.position.x + i / 2 * (i % 2 == 0 ? 0.5f : -0.5f), this.transform.position.y + 0.5f, 0);
			GameObject bl = Instantiate(bullet, pos, this.transform.rotation);
			bl.GetComponent<Bullet>().shooter = this.gameObject;
		}
	}

	public void PowerUp(int point)
	{
		if (powerLevel >= powerMax)
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

	public override void Damage(float damage)
	{
		health -= damage;
		gm.UpdateGameMainUI(health / maxHealth);
		if (health <= 0)
		{
			gm.GameClear();
		}
	}

	new public void Heal(float healHp)
	{
		base.Heal(healHp);
		gm.UpdateGameMainUI(hpParsent: health / maxHealth);
	}
}
