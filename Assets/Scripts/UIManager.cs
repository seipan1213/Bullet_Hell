using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public GameManager gm;
	[SerializeField]
	private Canvas gameClear;

	[SerializeField]
	private Canvas gameMain;

	[SerializeField]
	private TextMeshProUGUI resultScoreText;

	[SerializeField]
	private TextMeshProUGUI scoreText;

	[SerializeField]
	private Slider hpSlider;

	[SerializeField]
	private TextMeshProUGUI timer;

	void Start()
	{
		gameClear.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GameClear(int score)
	{
		gameClear.enabled = true;
		resultScoreText.text = "Score: " + score.ToString();
	}

	public void GameMain(int score = -1, float hpParsent = -1)
	{
		if (score >= 0)
		{
			scoreText.text = "Score: " + score.ToString();
		}
		if (hpParsent >= 0)
		{
			hpSlider.value = hpParsent;
		}
	}

	public void Timer(float time)
	{
		timer.text = "Timer: " + ((int)Mathf.Ceil(time)).ToString();
	}
}
