using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * EvasiveManeuver - The enemy's maneuvers (In Game)
 * 
 * GNU General Public License
 */
public class EvasiveManeuver : MonoBehaviour {

	// maneuver values
    public float dodge;
    public float smoothing;
    public float tilt;
 
	// maneuver times
	public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
 
	// maneuver boundary
	public Boundary boundary;

	// maneuver on X axis
	public bool evadeX;

	// maneuver on Y axis
	public bool evadeY;

	// private values
    private float currentSpeed;
    private float targetManeuverX;
	private float targetManeuverY;
    private Rigidbody rb;

	/**
	 * Start() - Called on initialization
	 */ 
    void Start () {
		// Attach RigidBody
        rb = GetComponent <Rigidbody> ();
		// Get current velocity on Z axis
        currentSpeed = rb.velocity.z;
		// Do maneuvers
        StartCoroutine (Evade ());
    }

	/**
	 * Evade() - Do the maneuvers operations
	 */ 
    IEnumerator Evade() {
		// Wait before begin the manoeuvers 
        yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        while (true) {
			// Calculate the maneuver on X and Y axis
			targetManeuverY = (evadeY ? Random.Range (1, dodge) * -Mathf.Sign (transform.position.y) : 0);
			targetManeuverX = (evadeX ? Random.Range (1, dodge) * -Mathf.Sign (transform.position.x) : 0);
			// Duartion of the maneuver
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			// Stop the maneuver
            targetManeuverY = 0;
			targetManeuverX = 0;
			// Time befor the next maneuver
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
        }
    }
    
	/**
	 * FixedUpdate() : Called at end of each frame
	 */ 
    void FixedUpdate () {
		// Calculate velocity on X and Y axis
		float newManeuverY = Mathf.MoveTowards (rb.velocity.y, targetManeuverY, Time.deltaTime * smoothing);
        float newManeuverX = Mathf.MoveTowards (rb.velocity.x, targetManeuverX, Time.deltaTime * smoothing);
		// Set velocity to the RigidBody
		rb.velocity = new Vector3 (newManeuverX, newManeuverY, currentSpeed);
		// The object must stay in the Boundary limits
        rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp (rb.position.y, boundary.xMin, boundary.xMax),
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
		// Rotate object on Z axis depend on velocity on X axis
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}