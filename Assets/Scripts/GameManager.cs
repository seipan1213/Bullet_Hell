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

		if (timeLimit <= 0 && !gameClear)
		{
			GameClear();
		}
		if (gameClear && Input.anyKeyDown)
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
		if (spawnDeltaTime >= spawnInterval)
		{
			int enemy_num = Random.Range(0, enemys.Length - 1);
			int spawn_num = Random.Range(0, enemySpawnPoint.Length - 1);
			GameObject enemy = Instantiate(enemys[enemy_num], enemySpawnPoint[spawn_num].position, enemySpawnPoint[spawn_num].rotation);
			enemy.GetComponent<Enemy>().targetBasePos = enemyBaseTarget.position;
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

	void AddScore(int score)
	{
		this.score += score;
	}
}
