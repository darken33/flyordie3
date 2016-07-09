using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	void Start ()
	{
		if (player != null) {
			offset = transform.position - player.transform.position;
		}
	}

	void LateUpdate ()
	{
		if (player != null) {
			transform.position = player.transform.position + offset;
			transform.rotation = player.transform.rotation;
		}
	}
}