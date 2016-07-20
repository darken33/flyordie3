using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject shieldActivation;
	public int scoreValue;
	private GameController gameController;
	private PlayerController playerController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Can't find GameController");
		}
		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController> ();
		}
		if (playerController == null) {
			Debug.Log ("Can't find PlayerController");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		// Dans le cas du Boundary 
		if (other.tag == "Boundary") {
			return;
		}
		// Dans le cas d'un Astéroid ou d'un Enemy
		else if (this.tag == "Asteroid" || this.tag == "Enemy") {
			// S'il est touché par un ennemy, un Astéroide ou un bonus ne rien faire
			if (other.tag == "Enemy" || other.tag == "Asteroid" || other.tag == "Shield_bonus" || other.tag == "Life_bonus" || other.tag == "Laser_bonus") {
				return;
			}
			// Sinon le faire exploser et le détruire
			else {
				Instantiate (explosion, transform.position, transform.rotation);
				// S'il est touché par un laser du joueur on incrémente le score
				if (other.tag == "Bolt_p") {
					gameController.AddScore (scoreValue);
					gameController.DecBonusRandom ();
				}
				// S'il est touché par le joueur fin du jeu
				if (other.tag == "Player") {
					Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
					if (gameController != null) {
						gameController.GameOver ();
					}
				}
				// S'il est touché par le bouclier désactiver celui-ci
				if (other.tag == "Shield") {
					Instantiate (shieldActivation, playerController.gameObject.transform.position, playerController.gameObject.transform.rotation);
					other.gameObject.SetActive (false);
				} 
				// Sinon on détruit l'objet touché
				else {
					Destroy(other.gameObject);
				}
				// On détruit l'Astéroide
				Destroy(gameObject);
				return;
			}
		}
		// Dans le cas d'un bonus Shield
		else if (this.tag == "Shield_bonus") {
			// S'il est touché par le joueur on active le bouclier
			if (other.tag == "Bolt_p" || other.tag == "Player") {
				Instantiate (explosion, this.transform.position, this.transform.rotation);
				Instantiate (shieldActivation, playerController.gameObject.transform.position, playerController.gameObject.transform.rotation);
				playerController.SetActiveShield ();
			} 
			// On détruit l'Astéroide
			Destroy(gameObject);
			return;
		}
		// Dans le cas d'un bonus Shield
		else if (this.tag == "Laser_bonus") {
			// S'il est touché par le joueur on active le bouclier
			if (other.tag == "Bolt_p" || other.tag == "Player") {
				Instantiate (explosion, this.transform.position, this.transform.rotation);
				playerController.IncLazers();
			} 
			// On détruit l'Astéroide
			Destroy(gameObject);
			return;
		}
		// Dans le cas d'un bonus Shield
		else if (this.tag == "Life_bonus") {
			// S'il est touché par le joueur on active le bouclier
			if (other.tag == "Bolt_p" || other.tag == "Player") {
				Instantiate (explosion, this.transform.position, this.transform.rotation);
				gameController.IncLives();
			} 
			// On détruit l'Astéroide
			Destroy(gameObject);
			return;
		}
		// Dans le cas dun lazer ennemy
		else if (this.tag == "Bolt_e") {
			// S'il touche le bouclier
			if (other.tag == "Shield") {
				Instantiate (shieldActivation, playerController.gameObject.transform.position, playerController.gameObject.transform.rotation);
				other.gameObject.SetActive(false);
			} 
			// S'il touche le joueur
			if (other.tag == "Player") {
				playerController.ResetLazers ();
				if (gameController.DecLives () <= 0) {
					Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
					if (gameController != null) {
						gameController.GameOver ();
					}
					Destroy (other.gameObject);
				} else {
					Instantiate (explosion, other.transform.position, other.transform.rotation);
				}
			}
			Destroy(gameObject);
			return;
		}
	}
}
