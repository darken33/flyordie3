using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * DestroyByTime - Destroy some objects that have a lifetime (In Game)
 * 
 * GNU General Public License
 */
public class DestroyByTime : MonoBehaviour {

	// LifeTime of the object
	public float lifeTime;

	/**
	 * Start() - Called on initialisation
	 */
	void Start () {
		Destroy (gameObject, lifeTime);
	}

}
