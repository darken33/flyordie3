using UnityEngine;
using System.Collections;

// Boundary Class for player min/max positions
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * PlayerController - Player actions to control his spacecraft
 * 
 * GNU General Public License
 */
public class PlayerController : MonoBehaviour
{
	// Speed of maneuvers X/Y
	public float speed;
	// Max angle
	public float tilt;
	// Player boundary
	public Boundary boundary;

	// Main laser
	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;
	// Secondaries lasers
	public GameObject shot2;
	public Transform[] shotSpawn2;
	// The shield
	public GameObject shield;

	// private values
	private MenuController menuController;
	private float nextFire;
	private Quaternion calibrationQuaternion;
	private Vector3 accelerationSnapshot;
	private int secondaryShots;

	/**
	 * Start() - Called during initialization
	 */
	void Start() {
		// No secondaries shots at start
		secondaryShots = 0;
		// No shield activated
		shield.SetActive (false);
		// Attach MenuController
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} else {
			// In Android mode or VR mode calibrate the accelerometer
			if (menuController.type == 2 || menuController.type == 3) {
				CalibrateAccelerometer ();
			}
		}
	}

	/**
	 * IncLazers() - increment secondaries shot spawns (Laser Item)
	 */
	public void IncLazers() {
		secondaryShots++;
		if (secondaryShots > 2) {
			secondaryShots = 2;
		}
	}

	/**
	 * ResetLazers() - reset secondaries shot spawns (When playe loose a life)
	 */
	public void ResetLazers() {
		secondaryShots = 0;
	}

	/**
	 * SetActiveShield()  - Activate the Shield (Shield Item)
	 */ 
	public void SetActiveShield() {
		shield.SetActive (true);
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
	 * Update() - Called on each Frame
	 */
	void Update() {
		// If player push fire button, or automatically (VR Mode) instanciate shots depending on the number of shotSpawns
		if ((Input.GetButton("Fire1") || menuController.type == 3) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			if (secondaryShots >= 1) {
				Instantiate(shot2, shotSpawn2[0].position, shotSpawn2[0].rotation);
			}
			if (secondaryShots >= 2) {
				Instantiate(shot2, shotSpawn2[1].position, shotSpawn2[1].rotation);
			}
		}
	}

	/**
	 * FixedUpdate() - Called at the end of each Frame
	 */
	void FixedUpdate () {
		float moveHorizontal = 0.0f; 
		float moveVertical = 0.0f; 
		// Get movement from Accelerometer (Android & VR Mode)
		if (menuController.type == 2 || menuController.type == 3) {
			Vector3 accelerationRaw = Input.acceleration;
			Vector3 acceleration = FixAcceleration(accelerationRaw);
			moveHorizontal = acceleration.x;
			moveVertical = acceleration.y;
		} 
		// Else get movement from Keyboard
		else {
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");
		}
		// Apply movement 
		Vector3 movement = new Vector3 (moveHorizontal, -moveVertical, 0.0f);
		GetComponent<Rigidbody>().velocity = movement * speed;
		// Correct movement with Boundary
		GetComponent<Rigidbody>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.xMin, boundary.xMax),
			0.0f 
		);
		// Apply rotation depending on X velocity
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (GetComponent<Rigidbody>().velocity.y * (-tilt/2), 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

}