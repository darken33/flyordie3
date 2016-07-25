using UnityEngine;
using System.Collections;

[System.Serializable]
public class BoundaryP
{
	public float xMin, xMax, yMin, yMax;
}

public class PointerController : MonoBehaviour {

	public float speed;
	public BoundaryP boundary;

	private MenuController menuController;
	private Quaternion calibrationQuaternion;
	private Vector3 accelerationSnapshot;
	private float zPosition;

	// Use this for initialization
	void Start () {
		zPosition = transform.position.z;
		gameObject.GetComponent<TextMesh> ().text = "";
		GameObject menuControllerObject = GameObject.FindWithTag ("MenuController");
		if (menuControllerObject != null) {
			menuController = menuControllerObject.GetComponent<MenuController> ();
		}
		if (menuController == null) {
			Debug.Log ("Can't find MenuController");
		}
		if (menuController.type == 3) {
			Activate ();
		}
	}

	public void Activate() {
		gameObject.SetActive (true);
		gameObject.GetComponent<TextMesh> ().text = "";
		if (menuController.type == 3) {
			StartCoroutine (InitiailizePointer ());
		}
	}

	IEnumerator InitiailizePointer() {
		yield return new WaitForSeconds (1f);
		transform.position = new Vector3(0.0f, 0.0f, zPosition);
		if (menuController.type == 2 || menuController.type == 3) {
			CalibrateAccelerometer ();
		}
		gameObject.GetComponent<TextMesh> ().text = "+";
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
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
				zPosition 
			);
	}

}
