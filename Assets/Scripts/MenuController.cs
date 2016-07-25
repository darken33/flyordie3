using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	// 0 standalone, 1 webGL, 2 android, 3 android-VR
	public int type;
	// d'ont show the bt_quit on WebGL mode
	public Component bt_quit;
	// Sound to play on click
	public Component soundClick;
	// Canvas Standard
	public Component canvas;
	// Canvas VR
	public Component canvasVR;

	/**
	 * On start
	 */ 
	public void Start() {
		// on Android VR mode display CanvasVR
		GameObject gvrObject = GameObject.FindWithTag ("GVR");
		GvrViewer gvrViewer = null;
		if (gvrObject != null) gvrViewer = gvrObject.GetComponent<GvrViewer> (); 
		if (type == 3) {
			if (gvrViewer != null)
				gvrViewer.VRModeEnabled = true;
			if (canvas != null)
				canvas.gameObject.SetActive (false);
			if (canvasVR != null) 
				canvasVR.gameObject.SetActive (true);
		}
		// else display Canvas
		else {
			if (gvrViewer != null)
				gvrViewer.VRModeEnabled = false;
			if (canvas != null)
				canvas.gameObject.SetActive (true);
			if (canvasVR != null) 
				canvasVR.gameObject.SetActive (false);
		}
		// hide Quit button on WebGL Mode
		if (type == 1) {
			if (bt_quit != null) {
				bt_quit.gameObject.SetActive (false);
			}
		}
	}

	/**
	 * Open the Scene Wait_Play
	 */ 
	public void WaitPlay() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Wait_Play");
	}

	public void OpenScene(string scene) {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene (scene);
	}

	public void PlayOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main3D");
	}

	public void HelpOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Player");
	}
		
	public void HistoryOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("History");
	}

	public void History1OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("History1");
	}

	public void History2OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("History2");
	}

	public void CreditsOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits");
	}

	public void Credits1OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits1");
	}

	public void Credits2OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits2");
	}

	public void Credits3OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits3");
	}

	public void Credits4OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits4");
	}

	public void Credits5OnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits5");
	}

	public void QuitOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		Application.Quit();
	}

	public void HomeOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Title");
	}

	public void NextAsteroidOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Asteroids");
	}

	public void NextUFOClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Ufo");
	}

	public void NextBomberClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Bomber");
	}

	public void NextInterceptorClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Interceptor");
	}

	public void NextBonusClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Help_Bonus");
	}
}
