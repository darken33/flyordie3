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
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;
	private MenuController menuController;

	public float fireRate;

	private float nextFire;
	private Quaternion calibrationQuaternion;
	private Vector3 accelerationSnapshot;

	void Start(){
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		} else {
			if (menuController.type == 2) {
				CalibrateAccelerometer ();
			}
		}
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
		if (menuController.type == 2) {
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
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}