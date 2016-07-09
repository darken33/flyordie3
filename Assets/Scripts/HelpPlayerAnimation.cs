using UnityEngine;
using System.Collections;

public class HelpPlayerAnimation : MonoBehaviour {

	public int speed;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent <Rigidbody> ();
		rb.angularVelocity = new Vector3(0.0f, speed, 0.0f);
	}

}
