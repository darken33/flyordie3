using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour 
{

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
	public bool evadeX;
	public bool evadeY;

    private float currentSpeed;
    private float targetManeuverX;
	private float targetManeuverY;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent <Rigidbody> ();
        currentSpeed = rb.velocity.z;
        StartCoroutine (Evade ());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true)
        {
			targetManeuverY = (evadeY ? Random.Range (1, dodge) * -Mathf.Sign (transform.position.y) : 0);
			targetManeuverX = (evadeX ? Random.Range (1, dodge) * -Mathf.Sign (transform.position.x) : 0);
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuverY = 0;
			targetManeuverX = 0;
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }
    
    void FixedUpdate ()
    {
		float newManeuverY = Mathf.MoveTowards (rb.velocity.y, targetManeuverY, Time.deltaTime * smoothing);
        float newManeuverX = Mathf.MoveTowards (rb.velocity.x, targetManeuverX, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuverX, newManeuverY, currentSpeed);
        rb.position = new Vector3 
        (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp (rb.position.y, boundary.xMin, boundary.xMax),
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}