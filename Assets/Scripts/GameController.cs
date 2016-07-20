using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// GameObjects
	public GameObject[] hazards;
	public GameObject[] bonus;
	public GUIText scoreText;
	public GUIText gameOverText;
	public GUIText restartText;
	public GUIText targetText;
	public GUIText waveText;
	public GUIText dieText;
	public GUIText livesText;
	public Component bt_no;
	public Component bt_yes;

	public int startEnemyType;
	public int bonusDec;
	public int bonusValue;
	public float startWait;
	public float spawnWait;
	public Vector3 spawnValues;
	public float waveWait;
	public int[] waveEnemyTypeIncrement;
	public int hazardIncrement;
	public int hazardStart;
	public int hazardMax;

	private int hazardCount;
	private int wave;
	private int score;
	private bool gameOver;
	private bool restart;
	private int numberEnemyTypes;
	private float scale;
	private int bonusRandom;
	private int lives;

	// Use this for initialization
	void Start () {
		scale = Screen.currentResolution.width / 854;
		lives = 5;
		livesText.fontSize = Mathf.RoundToInt(livesText.fontSize * scale);
		scoreText.fontSize = Mathf.RoundToInt(scoreText.fontSize * scale);
		waveText.fontSize = Mathf.RoundToInt(waveText.fontSize * scale);
		targetText.fontSize = Mathf.RoundToInt(targetText.fontSize * scale);
		dieText.fontSize = Mathf.RoundToInt(dieText.fontSize * scale);
		gameOverText.fontSize = Mathf.RoundToInt(gameOverText.fontSize * scale);
		restartText.fontSize = Mathf.RoundToInt(restartText.fontSize * scale);
		bt_no.gameObject.SetActive (false);
		bt_yes.gameObject.SetActive (false);
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		dieText.text = "";
		targetText.text = "+";
		score = 0;
		wave = 0;
		bonusRandom = 100;
		hazardCount = hazardStart;
		numberEnemyTypes = startEnemyType;
		UpdateWave ();
		UpdateScore ();
		UpdateLives ();
		StartCoroutine (SpawnWaves());
	}
		
	// Update is called once per frame
	void Update() {
		if (restart) {
			if(Input.GetKeyDown(KeyCode.Q)) {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Title");
			}
		}
	}

	public void DecBonusRandom() {
		bonusRandom -= bonusDec;
		if (bonusRandom < bonusValue) {
			bonusRandom = bonusValue;
		}
	}

	public int DecLives() {
		lives--;
		UpdateLives ();
		return lives;
	}

	public int IncLives() {
		lives++;
		if (lives > 5) {
			lives = 5;
		}
		UpdateLives ();
		return lives;
	}


	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);

		while (true) {
			wave++;
			UpdateWave ();
			for (int j = 0; j < waveEnemyTypeIncrement.Length; j++) {
				if (wave == waveEnemyTypeIncrement [j]) {
					numberEnemyTypes++;
				}
			}
			if (numberEnemyTypes > hazards.Length) {
				numberEnemyTypes = hazards.Length;
			}
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range(0, numberEnemyTypes)];
				if (bonusValue >= Random.Range (0, bonusRandom)) {
					bonusRandom = 100;
					hazard = bonus [Random.Range (0, bonus.Length)];
				}
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			hazardCount += hazardIncrement;
			if (hazardCount > hazardMax) {
				hazardCount = hazardMax;
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver) {
				bt_no.gameObject.SetActive (true);
				bt_yes.gameObject.SetActive (true);
				dieText.text = "You died on wave " + wave + ", your score is " + score;
				restartText.text = "Do you want to restart game ?";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int scoreValue) {
		score += scoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score : " + score;
	}
	void UpdateWave() {
		waveText.text = "Wave : " + wave;
	}
	void UpdateLives() {
		livesText.text = "Lives : " + lives;
	}
	public void GameOver(){
		lives = 0;
		UpdateLives ();
		targetText.text = "";
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
