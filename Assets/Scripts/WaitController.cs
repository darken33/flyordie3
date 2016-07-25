using UnityEngine;
using System.Collections;

public class WaitController : MonoBehaviour {

	public TextMesh timeText;
	public int waitingTime;
	public string scene;

	private int time;
	private MenuController menuController;

	// Use this for initialization
	void Start () {
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} 
		time = waitingTime;
		StartCoroutine (Waiting());	
	}
	
	IEnumerator Waiting () {
		for (time = waitingTime; time > 0; time--) {
			timeText.text = ""+time;
			yield return new WaitForSeconds (1f);
		}
		timeText.text = ""+time;
		menuController.OpenScene (scene);
	}

}
