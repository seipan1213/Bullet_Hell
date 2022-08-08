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

	private TextMeshProUGUI scoreText;
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
		scoreText.text = "Score: " + score.ToString();
	}
}
