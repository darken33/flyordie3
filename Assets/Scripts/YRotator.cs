using UnityEngine;
using System.Collections;

public class YRotator : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = new Vector3 (GetComponent<Rigidbody> ().angularVelocity.x, speed, GetComponent<Rigidbody> ().angularVelocity.z); 
	}

}
