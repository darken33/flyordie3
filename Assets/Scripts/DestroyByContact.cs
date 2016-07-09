using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Can't find GameController");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || (other.tag== "Bolt_e" && this.tag == "Enemy"))
		{
			return;
		}

		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}
		if (other.tag == "Bolt_p" && this.tag != "Bolt_e") {
			gameController.AddScore (scoreValue);
		}
		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			if (gameController != null) {
				gameController.GameOver ();
			}
		}
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
