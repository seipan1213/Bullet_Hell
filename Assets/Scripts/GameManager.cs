using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject[] enemys;
	[SerializeField]
	private Transform playerSpawnPoint;
	[SerializeField]
	private Transform[] enemySpawnPoint;

	[SerializeField]
	private Transform enemyBaseTarget;
	private float spawnDeltaTime = 0;

	[SerializeField]
	private float spawnInterval = 0.5f;

	public int spawnCurrent = 0;

	[SerializeField]
	private int spawnMax = 30;

	[SerializeField]
	private float timeLimit = 60f;

	public bool gameClear = false;

	[SerializeField]

	private UIManager um;

	private int score = 0;

	// Start is called before the first frame update
	void Start()
	{
		SpawnPlayer();
	}

	// Update is called once per frame
	void Update()
	{
		SpawnEnemy();
		um.Timer(timeLimit);
		if (timeLimit <= 0 && !gameClear)
		{
			GameClear();
		}
		if (gameClear && Input.GetKeyDown(KeyCode.Q))
		{
			SceneManager.LoadScene("Title");
		}
		timeLimit -= Time.deltaTime;
	}


	void SpawnPlayer()
	{
		Instantiate(player, playerSpawnPoint).GetComponent<PlayerController>().gm = this;
	}

	void SpawnEnemy()
	{
		spawnDeltaTime += Time.deltaTime;
		if (spawnDeltaTime >= spawnInterval && spawnCurrent < spawnMax)
		{
			int enemy_num = Random.Range(0, enemys.Length - 1);
			int spawn_num = Random.Range(0, enemySpawnPoint.Length - 1);
			Enemy enemy = Instantiate(enemys[enemy_num], enemySpawnPoint[spawn_num].position, enemySpawnPoint[spawn_num].rotation).GetComponent<Enemy>();
			enemy.targetBasePos = enemyBaseTarget.position;
			enemy.gm = this;
			spawnDeltaTime = 0;
			spawnCurrent++;
		}
	}

	public void GameClear()
	{
		gameClear = true;
		um.GameClear(score);
		Time.timeScale = 0;
	}

	public void UpdateGameMainUI(float hpParsent = -1)
	{
		um.GameMain(score: score, hpParsent: hpParsent);
	}

	void AddScore(int score)
	{
		this.score += score;
		UpdateGameMainUI();
	}


	public void DieEnemy(int score)
	{
		spawnCurrent--;
		this.score += score;
		UpdateGameMainUI();
	}
}
