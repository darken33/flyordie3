using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * InitialisationController - Scene that explain tu put smartphone in the cardboard (VR Only)
 * 
 * GNU General Public License
 */
public class InitialisationController : MonoBehaviour {

	// Time to wait
	public int waitingTime;
	// Time to open scene of calbration
	public int view3dTime;
	// Text to display
	public TextMesh text;
	// Text of the timer
	public TextMesh timeText;

	// Private values 
	private int time;
	private MenuController menuController;

	/**
	 * Start() - Called on initialization
	 */ 
	void Start () {
		text.text = "Put your phone in the cardboard, \nand put it on your Head";
		// Attach GameController
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		}
		// Launch timer
		StartCoroutine (Waiting());
	}

	/**
	 * Start() - Routine of the timer
	 */ 
	IEnumerator Waiting () {
		for (time = waitingTime; time > view3dTime; time--) {
			timeText.text = ""+time;
			// Wait 1 second
			yield return new WaitForSeconds (1f);
		}
		timeText.text = ""+time;
		// Open the scene Calibrate
		menuController.OpenScene ("Calibrate");
	}

}
