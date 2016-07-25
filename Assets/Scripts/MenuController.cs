using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * MenuController - action when a button menu is clicked
 * 
 * GNU General Public License
 */
public class MenuController : MonoBehaviour {

	// 0 standalone, 1 webGL, 2 android, 3 android-VR
	public int type;

	// don't show the bt_quit on WebGL mode
	public Component bt_quit;

	// Sound to play on click
	public Component soundClick;

	// Canvas Standard
	public Component canvas;

	// Canvas VR
	public Component canvasVR;

	/**
	 * Start() - called on intialisation
	 */ 
	public void Start() {
		// On Android VR display canvasVR
		if (type == 3) {
			if (canvas != null)
				canvas.gameObject.SetActive (false);
			if (canvasVR != null) 
				canvasVR.gameObject.SetActive (true);
		}
		// else display Canvas
		else {
			if (canvas != null)
				canvas.gameObject.SetActive (true);
			if (canvasVR != null) 
				canvasVR.gameObject.SetActive (false);
			// hide Quit button on WebGL Mode
			if (type == 1) {
				if (bt_quit != null) {
					bt_quit.gameObject.SetActive (false);
				}
			}
		}
	}

	/**
	 * WaitPlay() - Show the scene for Accelerometer calibration (VR only)
	 */ 
	public void WaitPlay() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Wait_Play");
	}

	/**
	 * OpenScene() - open the scene given as argument
	 */ 
	public void OpenScene(string scene) {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (scene);
	}

	/**
	 * PlayOnClick() - Enter in playing mode
	 */ 
	public void PlayOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main3D");
	}

	/**
	 * HelpOnClick() - Open the scene Help_Player
	 */ 
	public void HelpOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Player");
	}
		
	/**
	 * HistoryOnClick() - Open the scene History
	 */ 
	public void HistoryOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("History");
	}

	/**
	 * History1OnClick() - Open the scene History1
	 */ 
	public void History1OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("History1");
	}

	/**
	 * History2OnClick() - Open the scene History2
	 */ 
	public void History2OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("History2");
	}

	/**
	 * CreditsOnClick() - Open the scene Credits
	 */ 
	public void CreditsOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits");
	}

	/**
	 * Credits1OnClick() - Open the scene Credits1
	 */ 
	public void Credits1OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits1");
	}

	/**
	 * Credits2OnClick() - Open the scene Credits2
	 */ 
	public void Credits2OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits2");
	}

	/**
	 * Credits3OnClick() - Open the scene Credits3
	 */ 
	public void Credits3OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits3");
	}

	/**
	 * Credits4OnClick() - Open the scene Credits4
	 */ 
	public void Credits4OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits4");
	}

	/**
	 * Credits5OnClick() - Open the scene Credits5
	 */ 
	public void Credits5OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits5");
	}

	/**
	 * QuitOnClick() - Quit the game
	 */ 
	public void QuitOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		Application.Quit();
	}

	/**
	 * HomeOnClick() - Go back to Title
	 */ 
	public void HomeOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Title");
	}

	/**
	 * NextAsteroidOnClick() - Open the scene Help_Asteroids
	 */ 
	public void NextAsteroidOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Asteroids");
	}

	/**
	 * NextUFOClick() - Open the scene Help_Ufo
	 */ 
	public void NextUFOClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Ufo");
	}

	/**
	 * NextBomberClick() - Open the scene Help_Bomber
	 */ 
	public void NextBomberClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Bomber");
	}

	/**
	 * NextInterceptorOnClick() - Open the scene Help_Interceptor
	 */ 
	public void NextInterceptorClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Interceptor");
	}

	/**
	 * NextBonusClick() - Open the scene Help_Bonus
	 */ 
	public void NextBonusClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Bonus");
	}
}
