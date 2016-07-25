using UnityEngine;
using System.Collections;

public class InitialisationController : MonoBehaviour {

	public int waitingTime;
	public int view3dTime;
	public TextMesh timeText;
	public TextMesh text;

	private int time;
	private MenuController menuController;
	private GvrViewer gvrViewer;

	// Use this for initialization
	void Start () {
		text.text = "Put your phone in the cardboard, \nand put it on your Head";
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		}
		// Open Scene Title if we're not in Android VR mode
		else if (menuController.type != 3) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Title");
		}
		StartCoroutine (Waiting());
	}

	IEnumerator Waiting () {
		for (time = waitingTime; time > view3dTime; time--) {
			timeText.text = ""+time;
			yield return new WaitForSeconds (1f);
		}
		timeText.text = ""+time;
		menuController.OpenScene ("Calibrate");
	}

}
