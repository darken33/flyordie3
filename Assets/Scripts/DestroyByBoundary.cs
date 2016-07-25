using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * DestroyByBoundary - Destroy all object witch get out the Boundary (In Game)
 * 
 * GNU General Public License
 */
public class DestroyByBoundary : MonoBehaviour {
	
	/**
	 * OnTriggerExit() - Called when an object exit from Boundary
	 */
	void OnTriggerExit(Collider other) {
		// Do not destroy the Pointer for VR Mode
		if (other.tag != "Pointer") {
			Destroy (other.gameObject);
		}
	}
}