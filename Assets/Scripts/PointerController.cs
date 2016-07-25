using UnityEngine;
using System.Collections;

// Boundary Class for the pointer
[System.Serializable]
public class BoundaryP {
	public float xMin, xMax, yMin, yMax;
}

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * PointerController - Simulate a mouse pointer based on the accelerometer (VR Only)
 * 
 * GNU General Public License
 */
public class PointerController : MonoBehaviour {

	// pointer speed
	public float speed;
	// pointer boundary
	public BoundaryP boundary;

	// Private values
	private MenuController menuController;
	private Quaternion calibrationQuaternion;
	private Vector3 accelerationSnapshot;
	private float zPosition;

	/**
	 * Start() - Called during initialization
	 */ 
	void Start () {
		// Save the Z position
		zPosition = transform.position.z;
		// Clear the pointer
		gameObject.GetComponent<TextMesh> ().text = "";
		// Attach the MenuController
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		}
		// On VR Mode activate the pointer
		else if (menuController.type == 3) {
			Activate ();
		}
	}

	/**
	 * Activate() - Activate the pointer 
	 */ 
	public void Activate() {
		gameObject.SetActive (true);
		gameObject.GetComponent<TextMesh> ().text = "";
		// Initialization only in VR mode
		if (menuController.type == 3) {
			StartCoroutine (InitiailizePointer ());
		}
	}

	/**
	 * InitiailizePointer() - 
	 */ 
	IEnumerator InitiailizePointer() {
		// Wait 1 second
		yield return new WaitForSeconds (1f);
		// The calibrate the accelerometer
		CalibrateAccelerometer ();
		// Put the pointer in the center of the screen
		transform.position = new Vector3(0.0f, 0.0f, zPosition);
		gameObject.GetComponent<TextMesh> ().text = "+";
	}

	/**
	 * CalibrateAccelerometer() - Take a snapshot of the accelerometer values
	 */
	private void CalibrateAccelerometer() {
		accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	/**
	 * FixAcceleration() - Fix the accelerometer values with the snaphsot taken on CalibrateAccelerometer()
	 */
	private Vector3 FixAcceleration(Vector3 acceleration) {
		Vector3 accelerationFixed = calibrationQuaternion * acceleration;
		return accelerationFixed;
	}

	/**
	 * FixedUpdate() - Called at end of each frame
	 */
	void FixedUpdate ()
	{
		float moveHorizontal = 0.0f; 
		float moveVertical = 0.0f; 
		// Get movementfrom Accelerometer
		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = FixAcceleration(accelerationRaw);
		moveHorizontal = acceleration.x;
		moveVertical = acceleration.y;
		Vector3 movement = new Vector3 (moveHorizontal, -moveVertical, 0.0f);
		// Apply velocity to the rigid body and correct position
		GetComponent<Rigidbody>().velocity = movement * speed;
		GetComponent<Rigidbody>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
			zPosition 
		);
	}

}
