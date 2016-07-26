using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * GameController - The main controller of the game (In Game)
 * 
 * GNU General Public License
 */
[RequireComponent(typeof(Text))]
public class GameController : MonoBehaviour {

	// List of Asteroids and Enemies
	public GameObject[] hazards;
	// List of bonus items
	public GameObject[] bonus;

	// Canvas
	public Text score2Text;
	public TextMesh target2Text;
	public Text lives2Text;
	public Text wave2Text;
	public Text gameOver2Text;
	public Text die2Text;
	public Text restart2Text;
	public Component bt_no;
	public Component bt_yes;

	// Canvas VR (VR Only)
	public TextMesh score3Text;
	public TextMesh lives3Text;
	public TextMesh wave3Text;
	public TextMesh gameOver3Text;
	public TextMesh die3Text;
	public TextMesh restart3Text;
	public Component bt_no_vr;
	public Component bt_yes_vr;

	// The target 
	public GameObject target;
	// The pointer for UI interaction (VR Only)
	public GameObject pointer;

	// Number of enemies Type 
	public int startEnemyType;
	// Time before the 1st Wave
	public float startWait;
	// Time befor each enemy spawn
	public float spawnWait;
	// Time beetween two waves
	public float waveWait;
	// Enemy spawn position
	public Vector3 spawnValues;
	// Waves to increment the nomber of Enemies's type
	public int[] waveEnemyTypeIncrement;
	// Increment enemy number at each wave
	public int hazardIncrement;
	// Number of enemies at 1st wave
	public int hazardStart;
	// Nomber max of enemies
	public int hazardMax;
	// Bonus chance incerement on eatch enemy destroyed
	public int bonusDec;
	// Bonus chance
	public int bonusValue;

	// private Values
	private int score;
	private int lives;
	private int wave;
	private int hazardCount;
	private int numberEnemyTypes;
	private int bonusRandom;
	private bool gameOver;
	private bool restart;
	private MenuController menuController;

	// Use this for initialization
	void Start () {
		// Unactive the pointer (VR Only)
		pointer.SetActive (false);
		// Attach MenuController
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		}
		// Initialization score, lives, wave, ...
		lives = 5;
		score = 0;
		wave = 0;
		bonusRandom = 100;
		hazardCount = hazardStart;
		numberEnemyTypes = startEnemyType;
		gameOver = false;
		restart = false;
		// Update Canvas
		bt_no.gameObject.SetActive (false);
		bt_yes.gameObject.SetActive (false);
		restart2Text.text = "";
		gameOver2Text.text = "";
		die2Text.text = "";
		// Update Canvas VR (VR Only)
		bt_no_vr.gameObject.SetActive (false);
		bt_yes_vr.gameObject.SetActive (false);
		restart3Text.text = "";
		gameOver3Text.text = "";
		die3Text.text = "";
		// Target
		target2Text.text = "+";
		target.SetActive (true);
		// Updates Texts
		UpdateWave ();
		UpdateScore ();
		UpdateLives ();
		// Start the SpawnWaves routine
		StartCoroutine (SpawnWaves());
	}

	/**
	 * DecBonusRandom() - To increase chance to have a bonus item
	 */
	public void DecBonusRandom() {
		bonusRandom -= bonusDec;
		if (bonusRandom < bonusValue) {
			bonusRandom = bonusValue;
		}
	}

	/**
	 * DecLives() - decrease lives of player
	 */
	public int DecLives() {
		lives--;
		UpdateLives ();
		return lives;
	}

	/**
	 * IncLives() - increase lives of player
	 */
	public int IncLives() {
		lives++;
		if (lives > 5) {
			lives = 5;
		}
		UpdateLives ();
		return lives;
	}


	/**
	 * SpawnWaves() - Routine to spawn enemies on waves
	 */
	IEnumerator SpawnWaves () {
		// Wait before launching the 1st wave
		yield return new WaitForSeconds (startWait);
		// Infinite loop
		while (true) {
			wave++;
			UpdateWave ();
			// If the wave number is in the waveEnemyTypeIncrement array
			// increment the type of enemies
			for (int j = 0; j < waveEnemyTypeIncrement.Length; j++) {
				if (wave == waveEnemyTypeIncrement [j]) {
					numberEnemyTypes++;
				}
			}
			if (numberEnemyTypes > hazards.Length) {
				numberEnemyTypes = hazards.Length;
			}
			// Create a wave of enemies	
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range(0, numberEnemyTypes)];
				if (bonusValue >= Random.Range (0, bonusRandom)) {
					bonusRandom = 100;
					hazard = bonus [Random.Range (0, bonus.Length)];
				}
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				// Wait before spawn another enemy
				yield return new WaitForSeconds (spawnWait);
			}
			// Increment the number of enemies
			hazardCount += hazardIncrement;
			if (hazardCount > hazardMax) {
				hazardCount = hazardMax;
			}
			// Wait before next wave
			yield return new WaitForSeconds (waveWait);
			// On game over
			if (gameOver) {
				// Update Canvas
				bt_no.gameObject.SetActive (true);
				bt_yes.gameObject.SetActive (true);
				die2Text.text = "You died on wave " + wave + ", your score is " + score;
				restart2Text.text = "Do you want to restart game ?";
				// Update Canvas VR
				bt_no_vr.gameObject.SetActive (true);
				bt_yes_vr.gameObject.SetActive (true);
				die3Text.text = "You died on wave " + wave + ", your score is " + score;
				restart3Text.text = "Do you want to restart game ?";
				// Try to activate the pointer (VR Only)
				if (menuController.type == 3) {
					PointerController pointerController = pointer.GetComponent<PointerController> ();
					pointerController.Activate ();
				}
				// Exit the infinite loop
				restart = true;
				break;
			}
		}
	}

	/**
	 * AddScore() - Increment the player score
	 */
	public void AddScore(int scoreValue) {
		score += scoreValue;
		UpdateScore ();
	}

	/**
	 * UpdateScore() - Update score text
	 */ 
	void UpdateScore() {
		score2Text.text = "Score : " + score;
		score3Text.text = "Score : " + score;
	}

	/**
	 * UpdateWave() - Update wave text
	 */ 
	void UpdateWave() {
		wave2Text.text = "Wave : " + wave;
		wave3Text.text = "Wave : " + wave;
	}

	/**
	 * UpdateLives() - Update lives text
	 */ 
	void UpdateLives() {
		lives2Text.text = "Lives : " + lives;
		lives3Text.text = "Lives : " + lives;
	}

	/**
	 * GameOver() - End of the game
	 */ 
	public void GameOver(){
		lives = 0;
		UpdateLives ();
		// Unactive target
		target2Text.text = "";
		target.SetActive (false);
		// Update Game Over text
		gameOver2Text.text = "Game Over";
		gameOver3Text.text = "Game Over";
		gameOver = true;
	}
}
