using UnityEngine;
using System.Collections;

public class ClickController : MonoBehaviour {

	private MenuController menuController;
	private string colliderName;
	private string objectName;

	// Use this for initialization
	void Start () {
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} 
	}
	
	void OnTriggerEnter(Collider other) 
	{
		colliderName = other.tag;
		objectName = other.gameObject.name;
		Debug.Log (colliderName);
		StartCoroutine (ClickObject());
	}

	void OnTriggerExit() 
	{
		colliderName = "";
		objectName = "";
	}

	IEnumerator ClickObject () {
		yield return new WaitForSeconds (0.5f);
		if (colliderName == "NO_bt") {
			menuController.HomeOnClick ();
		} else if (colliderName == "YES_bt") {
			menuController.WaitPlay();
		} else if (colliderName == "PLAY_bt") {
			menuController.WaitPlay();
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
			Debug.Log (objectName);
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
