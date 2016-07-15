using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	// 0 standalone, 1 webGL, 2 android
	public int type;
	public Component bt_quit;
	public Component soundClick;
	public GUIText titleText;
	private float scale;
	public void Start() {
		// Ne pas afficher le bouton quit en mode web
		if (type == 1) {
			if (bt_quit != null) {
				bt_quit.gameObject.SetActive (false);
			}
		}
		// Taille de la police de Titre
		scale = Screen.currentResolution.width / 854;
		if (titleText != null) {
			titleText.fontSize = Mathf.RoundToInt (titleText.fontSize * scale);
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
}
