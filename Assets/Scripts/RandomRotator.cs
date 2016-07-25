using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * RandomRotator - Rondom rotation animation for the Asteroids
 * 
 * GNU General Public License
 */
public class RandomRotator : MonoBehaviour {

	// Angular of rotation
	public float tumble;

	/**
	 * Start() - Called during initialization
	 */
	void Start () {
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble; 
	}
}