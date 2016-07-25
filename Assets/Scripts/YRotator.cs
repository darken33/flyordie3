using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * YRotator - Rotation animation fore the bonus items
 * 
 * GNU General Public License
 */
public class YRotator : MonoBehaviour {

	// Rotation speed
	public float speed;

	/**
	 * Start() - Called during initialization
	 */ 
	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (GetComponent<Rigidbody> ().angularVelocity.x, speed, GetComponent<Rigidbody> ().angularVelocity.z); 
	}

}
