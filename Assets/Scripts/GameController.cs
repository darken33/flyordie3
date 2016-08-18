using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Scores Values
[System.Serializable]
public class Scoring
{
	public int score;
	public int date;
}

// Scores Values from server
[System.Serializable]
public class ScoringWeb
{
	public string date;
	public string score;
	public string rank;
}

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
	public Text shields2Text;
	public Text wave2Text;
	public Text gameOver2Text;
	public Text die2Text;
	public Text restart2Text;
	public Text newRecord2Text;
	public Component bt_no;
	public Component bt_yes;

	// Canvas VR (VR Only)
	public TextMesh score3Text;
	public TextMesh lives3Text;
	public TextMesh shields3Text;
	public TextMesh wave3Text;
	public TextMesh gameOver3Text;
	public TextMesh die3Text;
	public TextMesh restart3Text;
	public TextMesh newRecord3Text;
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
	private int shield;
	private int hazardCount;
	private int numberEnemyTypes;
	private int bonusRandom;
	private bool gameOver;
	private bool restart;
	private MenuController menuController;
	private string record;
	private string bestScoreRank;
	private Scoring lastGame;

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
		lives = 100;
		score = 0;
		wave = 0;
		shield = 0;
		bestScoreRank = "";
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
		newRecord2Text.text = "";
		newRecord2Text.gameObject.SetActive (false);
		// Update Canvas VR (VR Only)
		bt_no_vr.gameObject.SetActive (false);
		bt_yes_vr.gameObject.SetActive (false);
		newRecord3Text.text = "";
		newRecord3Text.gameObject.SetActive (false);
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
	public int DecLives(int value) {
		lives-=value;
		if (lives < 0) {
			lives = 0;
		}
		UpdateLives ();
		return lives;
	}

	/**
	 * DecShields() - decrease Shields of player
	 */
	public int DecShields(int value) {
		shield-=value;
		int returnValue = shield;
		if (shield < 0) {
			shield = 0;
		}
		UpdateShields ();
		return returnValue;
	}

	/**
	 * IncLives() - increase lives of player
	 */
	public int IncLives() {
		lives+=10;
		if (lives > 100) {
			lives = 100;
		}
		UpdateLives ();
		return lives;
	}

	/**
	 * IncLives() - increase lives of player
	 */
	public int IncShields() {
		shield+=25;
		if (shield > 100) {
			shield = 100;
		}
		UpdateShields();
		return shield;
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
				newRecord2Text.gameObject.SetActive (true);
				// Update Canvas VR
				bt_no_vr.gameObject.SetActive (true);
				bt_yes_vr.gameObject.SetActive (true);
				die3Text.text = "You died on wave " + wave + ", your score is " + score;
				restart3Text.text = "Do you want to restart game ?";
				newRecord3Text.gameObject.SetActive (true);
				// Try to activate the pointer (VR Only)
				if (menuController.type == 3) {
					PointerController pointerController = pointer.GetComponent<PointerController> ();
					pointerController.Activate ();
				}
				// Exit the infinite loop
				restart = true;
				break;
			} 
			// If player survive on the wave increase the bonus chance
			DecBonusRandom ();
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
		lives2Text.text = "Armor : " + lives + "%";
		lives3Text.text = "Armor : " + lives + "%";
	}

	/**
	 * UpdateShields() - Update shield text
	 */ 
	void UpdateShields() {
		shields2Text.text = "Shield : " + shield + "%";
		shields3Text.text = "Shield : " + shield + "%";
	}

	/**
	 * GameOver() - End of the game
	 */ 
	public void GameOver(){
		lives = 0;
		UpdateLives ();
		shield = 0;
		UpdateShields();
		// Unactive target
		target2Text.text = "";
		target.SetActive (false);
		// Update Game Over text
		gameOver2Text.text = "Game Over";
		gameOver3Text.text = "Game Over";
		gameOver = true;
		string date = ""+System.DateTime.Now.Year+(System.DateTime.Now.Month < 10 ? "0":"")+System.DateTime.Now.Month+(System.DateTime.Now.Day < 10 ? "0":"")+System.DateTime.Now.Day;
		int dateNum = int.Parse (date);
		// Save this game
		lastGame = new Scoring ();
		lastGame.score = score;
		lastGame.date = dateNum;
		Debug.Log (JsonUtility.ToJson (lastGame));
		PlayerPrefs.SetString("lastGame", JsonUtility.ToJson(lastGame));
		// Check personal record today
		Scoring todayGame = null;
		if (PlayerPrefs.HasKey ("todayGame")) {
			todayGame = JsonUtility.FromJson<Scoring> (PlayerPrefs.GetString ("todayGame"));
		} 
		if (todayGame == null || score > todayGame.score  || lastGame.date != todayGame.date) {
			PlayerPrefs.SetString("todayGame", JsonUtility.ToJson(lastGame));
		}
		// Check personal record
		Scoring bestGame = null;
		if (PlayerPrefs.HasKey ("bestGame")) {
			bestGame = JsonUtility.FromJson<Scoring> (PlayerPrefs.GetString ("bestGame"));
		} 
		if (bestGame == null || score > bestGame.score) {
			PlayerPrefs.SetString("bestGame", JsonUtility.ToJson(lastGame));
			newRecord2Text.text = "NEW PERSONAL RECORD !!!\n";
			newRecord3Text.text = "NEW PERSONAL RECORD !!!\n";
		}
		// Save score to server
		WWWForm data = new WWWForm ();
		data.AddField ("key", RestServicesConstants.KEY);
		data.AddField ("score", lastGame.score);
		data.AddField ("date", lastGame.date);
		StartCoroutine (CallServiceStorage (data));
	}

	IEnumerator CallServiceStorage(WWWForm data) {
		WWW post_service = new WWW (RestServicesConstants.STORAGE_SERVICE_URL, data);
		yield return post_service;
		if (post_service.error != null) {
			Debug.Log (post_service.error);
		}
		// Compare score to the server
		StartCoroutine (CallServiceGet ());
	}

	IEnumerator CallServiceGet() {
		WWW get_service = new WWW (RestServicesConstants.GET_SERVICE_URL+"?key="+RestServicesConstants.KEY+"&score="+score);
		yield return get_service;
		if (get_service.error == null) {
			string result2 = get_service.text;
			string[] res = result2.Split ('#');
			ScoringWeb scw = new ScoringWeb ();
			scw.date = res [0];
			scw.score = res [1];
			scw.rank = res [2];
			//	JsonUtility.FromJson<ScoringWeb> (result2);
			if (score >= int.Parse (scw.score)) {
				newRecord2Text.text = "NEW WORLD RECORD !!!\n";
				newRecord3Text.text = "NEW WORLD RECORD !!!\n";
			}
			newRecord2Text.text = newRecord2Text.text + "BEST SCORE IS " + scw.score + " YOUR RANK IS " + scw.rank;
			newRecord3Text.text = newRecord3Text.text + "BEST SCORE IS " + scw.score + " YOUR RANK IS " + scw.rank;
		} else {
			Debug.Log (get_service.error);
		}
	}
}
