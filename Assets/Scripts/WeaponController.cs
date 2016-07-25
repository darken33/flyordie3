using UnityEngine;
using System.Collections;

/**
 * Fly Or Die 3 by Philippe Bousquet <darken33@free.fr>
 * WeaponController - Automatic weapon fire
 * 
 * GNU General Public License
 */
public class WeaponController : MonoBehaviour {

	// Enemy's laser object
    public GameObject shot;
	// Shot Spawn
    public Transform shotSpawn;
	// Delay before autofire
	public float delay;
	// Fire rating
    public float fireRate;

	/**
	 * Satert() - Called during initialization
	 */ 
    void Start () {
        InvokeRepeating ("Fire", delay, fireRate);
    }

	/**
	 * Fire() - instantiate a laser
	 */
    void Fire () {
        Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
    }
}