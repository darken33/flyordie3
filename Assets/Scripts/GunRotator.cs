using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * GunRotator - Rotate the gun of the Bomber
 * 
 * GNU General Public License
 */
public class GunRotator : MonoBehaviour {

	// Max Angle of rotation
	public float maxRotation;
	// Delay beetwwen each rotate increment
	public float delay;
	// Angle of each roate increment
	public float tumble;

	// Private Value;
	private float rotation;
	private float increment;

	/**
	 * Start() - Called on initialization
	 */ 
	void Start () {
		rotation = 0;
		// Determine rotation direction
		increment = ((int)Random.Range(0,2) == 0 ? 1 : -1) * tumble;
		// Start the rotation routine
		StartCoroutine (RotateGun());
	}

	/**
	 * FixedUpdate() - Called at end of each frame
	 */ 
	void FixedUpdate () {
		GetComponent<Transform>().rotation = Quaternion.Euler (0.0f, 180+rotation, 0.0f);
	}

	/**
	 * RotateGun() - Routine of gun rotation
	 */
	IEnumerator RotateGun () {
		// Infinite loop
		while (true) {
			// Wait for next rotation increment
			yield return new WaitForSeconds (delay);
			// If max angle change direction
			if (rotation <= -maxRotation || rotation >= maxRotation) {
				increment *= -1;
			}
			// Increment rotation
			rotation += increment;
		}
	}
}