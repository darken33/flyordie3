using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * HelpPlayerAnimation - Animation of the player's spacecraft on Help scene
 * 
 * GNU General Public License
 */
public class HelpPlayerAnimation : MonoBehaviour {

	// Speed of rotation
	public int speed;

	// The Rigidbody
	private Rigidbody rb;

	/**
	 * Start() - Called on initialization
	 */ 
	void Start () {
		rb = GetComponent <Rigidbody> ();
		rb.angularVelocity = new Vector3(0.0f, speed, 0.0f);
	}

}
