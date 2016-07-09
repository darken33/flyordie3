using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// GameObjects
	public GameObject[] hazards;
	public GUIText scoreText;
	public GUIText gameOverText;
	public GUIText restartText;
	public GUIText targetText;
	public GUIText waveText;
	public GUIText dieText;
	public Component bt_no;
	public Component bt_yes;

	public int startEnemyType;
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

	// Use this for initialization
	void Start () {
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
		hazardCount = hazardStart;
		numberEnemyTypes = startEnemyType;
		UpdateWave ();
		UpdateScore ();
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

	public void GameOver(){
		targetText.text = "";
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
