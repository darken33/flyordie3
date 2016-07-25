using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * WaitController - Wait before opens the next scene for calibration (VR Only)
 * 
 * GNU General Public License
 */
public class WaitController : MonoBehaviour {

	// Timer text
	public TextMesh timeText;
	// Daly before opening next scene
	public int waitingTime;
	// Scene to open
	public string scene;

	// Private values
	private int time;
	private MenuController menuController;

	/**
	 * Start() - Called during initialization
	 */ 
	void Start () {
		// Attach the Menu Controller
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} 
		// Start the routine Waiting
		StartCoroutine (Waiting());	
	}

	/**
	 * Waiting() - Waiting waitingTime seconds before opening next scene
	 */ 
	IEnumerator Waiting () {
		for (time = waitingTime; time > 0; time--) {
			timeText.text = ""+time;
			yield return new WaitForSeconds (1f);
		}
		timeText.text = ""+time;
		menuController.OpenScene (scene);
	}

}
