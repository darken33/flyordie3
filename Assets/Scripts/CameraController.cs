using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * CameraController - The camera has to follow the Player
 * 
 * GNU General Public License
 */
public class CameraController : MonoBehaviour {

	// Player Object
	public GameObject player;

	// Offset beetwen Camera and Player
	private Vector3 offset;

	/**
	 * Start() - Called on initialisation
	 */
	void Start () {
		// Calculate the offset beetwen Camera and Player
		if (player != null) {
			offset = transform.position - player.transform.position;
		}
	}

	/**
	 * LateUpdate() - Called at the and of a frame
	 */ 
	void LateUpdate () {
		// Move the Camera like the Player (position and rotation)
		if (player != null) {
			transform.position = player.transform.position + offset;
			transform.rotation = player.transform.rotation;
		}
	}
}