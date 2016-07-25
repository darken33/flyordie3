using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * Mover - Auto move for hazards, items, lasers, ...
 * 
 * GNU General Public License
 */
public class Mover : MonoBehaviour {

	// Speed for objects on the Z axis
	public float speed;

	/**
	 * Start() - Called on initialization
	 */ 
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}