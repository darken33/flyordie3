using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * ShieldController - Rotate Shield
 * 
 * GNU General Public License
 */
public class ShieldController : MonoBehaviour {

	// Speed of rotation
	public float speed;

	// Actual rotation angle
	private float rotation;

	/**
	 * Start() - Called during initialization
	 */ 
	void Start() {
		rotation = 0.0f;
	}

	/**
	 * FixedUpdate () - Called at the end of each frame
	 */ 
	void FixedUpdate () {
		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotation, transform.rotation.z);
		rotation += speed;
	}
}