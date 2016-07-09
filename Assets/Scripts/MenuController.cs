using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public int type;
	public Component bt_quit;
	public Component soundClick;

	public void Start() {
		// Ne pas afficher le bouton quit en mode web
		if (type == 1) {
			if (bt_quit != null) {
				bt_quit.gameObject.SetActive (false);
			}
		}
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

	public void CreditsOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Credits");
	}

	public void QuitOnClick() {
		soundClick.GetComponent<AudioSource> ().Play ();
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
}
