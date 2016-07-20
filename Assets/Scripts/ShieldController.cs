using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	public float speed;
	private float rotation;
	void Start() {
		rotation = 0.0f;
	}

	void FixedUpdate ()
	{
		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotation, transform.rotation.z);
		rotation += speed;
	}
}