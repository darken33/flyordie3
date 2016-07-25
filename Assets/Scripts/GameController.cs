using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class GameController : MonoBehaviour {

	// GameObjects
	public GameObject[] hazards;
	public GameObject[] bonus;
	public Text score2Text;
	public Text target2Text;
	public Text lives2Text;
	public Text wave2Text;
	public Text gameOver2Text;
	public Text die2Text;
	public Text restart2Text;
	public TextMesh score3Text;
	public TextMesh lives3Text;
	public TextMesh wave3Text;
	public TextMesh gameOver3Text;
	public TextMesh die3Text;
	public TextMesh restart3Text;
	public GameObject target;
	public GameObject pointer;

	public Component bt_no;
	public Component bt_yes;
	public Component bt_no_vr;
	public Component bt_yes_vr;

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
		pointer.SetActive (false);
		lives = 5;
		bt_no.gameObject.SetActive (false);
		bt_yes.gameObject.SetActive (false);
		bt_no_vr.gameObject.SetActive (false);
		bt_yes_vr.gameObject.SetActive (false);
		gameOver = false;
		restart = false;
		restart2Text.text = "";
		restart3Text.text = "";
		gameOver2Text.text = "";
		gameOver3Text.text = "";
		die2Text.text = "";
		die3Text.text = "";
		target2Text.text = "+";
		target.SetActive (true);
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
				bt_no_vr.gameObject.SetActive (true);
				bt_yes_vr.gameObject.SetActive (true);
				die2Text.text = "You died on wave " + wave + ", your score is " + score;
				restart2Text.text = "Do you want to restart game ?";
				die3Text.text = "You died on wave " + wave + ", your score is " + score;
				restart3Text.text = "Do you want to restart game ?";
				PointerController pointerController = pointer.GetComponent<PointerController> ();
				pointerController.Activate ();
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
		score2Text.text = "Score : " + score;
		score3Text.text = "Score : " + score;
	}
	void UpdateWave() {
		wave2Text.text = "Wave : " + wave;
		wave3Text.text = "Wave : " + wave;
	}
	void UpdateLives() {
		lives2Text.text = "Lives : " + lives;
		lives3Text.text = "Lives : " + lives;
	}
	public void GameOver(){
		lives = 0;
		UpdateLives ();
		target2Text.text = "";
		target.SetActive (false);
		gameOver2Text.text = "Game Over";
		gameOver3Text.text = "Game Over";
		gameOver = true;
	}
}
