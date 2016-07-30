using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * ClickController - Simulate the action Click on VR Mode (VR Only)
 * 
 * GNU General Public License
 */
public class ClickController : MonoBehaviour {

	// The menu 
	private MenuController menuController;

	// Tag of the collider 
	private string colliderName;

	// Name of the object
	private string objectName;

	/**
	 * Start() - Called on initialisation
	 */ 
	void Start () {
		// Attach the MenuController
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} 
	}

	/**
	 * OntTriggerEnter() - Called when the point enter in collision with other collider
	 */ 
	void OnTriggerEnter(Collider other) {
		// Save Tag and object name
		colliderName = other.tag;
		objectName = other.gameObject.name;
		// Call ClickOject (after 0.5 second if the object is also in collision with the pointer Click)
		StartCoroutine (ClickObject());
	}

	/**
	 * OnTriggerExit() - Called when the point get out collision
	 */
	void OnTriggerExit() {
		// Reset Tag and Object Name
		colliderName = "";
		objectName = "";
	}

	/**
	 * ClickObject() - Action click on an object
	 */
	IEnumerator ClickObject () {
		// Wait 0.5 seconds
		yield return new WaitForSeconds (0.5f);
		// Action to perform
		if (colliderName == "NO_bt") {
			menuController.HomeOnClick ();
		} else if (colliderName == "YES_bt") {
			menuController.WaitPlay();
		} else if (colliderName == "PLAY_bt") {
			menuController.WaitPlay();
		} else if (colliderName == "SCORES_bt") {
			menuController.ScoresOnClick();
		} else if (colliderName == "HELP_bt") {
			menuController.HelpOnClick();
		} else if (colliderName == "HISTORY_bt") {
			menuController.HistoryOnClick();
		} else if (colliderName == "CREDIT_bt") {
			menuController.CreditsOnClick();
		} else if (colliderName == "QUIT_bt") {
			menuController.QuitOnClick();
		} else if (colliderName == "HOME_bt") {
			menuController.HomeOnClick();
		} else if (colliderName == "NEXT_bt") {
			// For the NEXT_bt Tag we have many Objects
			if (objectName == "Next_Player") {
				menuController.NextAsteroidOnClick();
			}
			else if (objectName == "Next_Asteroid") {
				menuController.NextUFOClick();
			}
			else if (objectName == "Next_UFO") {
				menuController.NextBomberClick();
			}
			else if (objectName == "Next_Bomber") {
				menuController.NextInterceptorClick();
			}
			else if (objectName == "Next_Interceptor") {
				menuController.NextBonusClick();
			}
			else if (objectName == "Next_History") {
				menuController.History1OnClick();
			}
			else if (objectName == "Next_History1") {
				menuController.History2OnClick();
			}
			else if (objectName == "Next_Credits") {
				menuController.Credits1OnClick();
			}
			else if (objectName == "Next_Credits1") {
				menuController.Credits2OnClick();
			}
			else if (objectName == "Next_Credits2") {
				menuController.Credits3OnClick();
			}
			else if (objectName == "Next_Credits3") {
				menuController.Credits4OnClick();
			}
			else if (objectName == "Next_Credits4") {
				menuController.Credits5OnClick();
			}
		}
	}

}
