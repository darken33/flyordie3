using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public GameObject shot2;
	public Transform[] shotSpawn2;

	public GameObject shield;

//	public SimpleTouchPad touchPad;
//	public SimpleTouchAreaButton areaButton;
	private MenuController menuController;

	public float fireRate;

	private float nextFire;
	private Quaternion calibrationQuaternion;
	private Vector3 accelerationSnapshot;
	private int secondaryShots;

	void Start(){
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		secondaryShots = 0;
		shield.SetActive (false);
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} else {
			if (menuController.type == 2 || menuController.type == 3) {
				CalibrateAccelerometer ();
			}
		}
	}
	public void IncLazers() {
		secondaryShots++;
		if (secondaryShots > 2) {
			secondaryShots = 2;
		}
	}
	public void ResetLazers() {
		secondaryShots = 0;
	}
	public void SetActiveShield() {
		shield.SetActive (true);
	}
	void CalibrateAccelerometer() {
		accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration(Vector3 acceleration) {
		Vector3 accelerationFixed = calibrationQuaternion * acceleration;
		return accelerationFixed;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = 0.0f; //Input.GetAxis ("Horizontal");
		float moveVertical = 0.0f; //Input.GetAxis ("Vertical");
		// Input mobile
		if (menuController.type == 2 || menuController.type == 3) {
			Vector3 accelerationRaw = Input.acceleration;
			Vector3 acceleration = FixAcceleration(accelerationRaw);
			moveHorizontal = acceleration.x;
			moveVertical = acceleration.y;
		} 
		// Input Desktop
		else {
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");
		}
		Vector3 movement = new Vector3 (moveHorizontal, -moveVertical, 0.0f);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.zMin, boundary.zMax),
				0.0f 
			);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (GetComponent<Rigidbody>().velocity.y * (-tilt/2), 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

	void Update()
	{
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
}