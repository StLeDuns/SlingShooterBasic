using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour
{

	// Fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public float velocityMult = 4f;

	public Image PowerUp;
	

	// Fields set dynamically
	private GameObject launchPoint;
	private Vector3 launchPos;
	private GameObject projectile;
	public static bool aimingMode;
	private int timer = 150;
	private static bool powerup;
	public static bool loadAni;
	public static bool shootAni;
	public AudioClip[] audioClip;

	void Awake ()
	{
		//print ("Awake()");
		Transform launchPointTrans = transform.FindChild ("LaunchPoint");

		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	
		powerup = false;
		loadAni = false;
		shootAni = false;

		PowerUp.color = new Color (1f, 1f, 1f, 0f);
	}
	
	void OnMouseEnter ()
	{
		//print ("Enter");
		launchPoint.SetActive (true);
	}
	
	void OnMouseExit ()
	{
		//print ("Exit");
		if (!aimingMode) 
			launchPoint.SetActive (false);
	}
	
	void OnMouseDown ()
	{
		//print ("Down");


		DestroyImmediate (projectile);
		// Player pressed mouse while over Slingshot
		aimingMode = true;

		// Instantiate a projectile
		projectile = Instantiate (prefabProjectile) as GameObject;

		// Start it at launch position
		projectile.transform.position = launchPos;

		// Set it to kinematic for now
		projectile.GetComponent<Rigidbody> ().isKinematic = true;

		loadAni = true; 
		timer = 150;


	}

	void Update ()
	{
		// If the Slingshot is not in aiming mode, don't run this code


		if (!aimingMode)
			return;

		 timer--;
		if (timer == 0) {
			powerup = true;
			PowerUp.color = new Color (1f, 1f, 1f, 1f);

		}


		// Get the current mouse position in 2D screen coordinates
		Vector3 mousePos = Input.mousePosition;
		// Convert the mouse position to 3D world coordinates
		mousePos.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos);

		// Find the delta from launch position to 3D mouse position
		Vector3 mouseDelta = mousePos3D - launchPos;

		// Limit mouseDelta to the radius of the Slingshot SphereCollider
		float maxMagnitude = GetComponent<SphereCollider> ().radius;
		mouseDelta = Vector3.ClampMagnitude (mouseDelta, maxMagnitude);

		// Now move the projectile to this new position
		projectile.transform.position = launchPos + mouseDelta;
		
		if (Input.GetMouseButtonUp (0)) {

			launchPoint.SetActive (false);
			// The mouse has been released
			aimingMode = false;

			PowerUp.color = new Color (1f, 1f, 1f, 0f);
			// Fire off the projectile with given velocity
			projectile.GetComponent<Rigidbody> ().isKinematic = false;

			if (powerup == true) {
				projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocityMult * 2f;
			} else {
				projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocityMult;
			}

			// Set the Followcam's target to our projectile
			FollowCam.S.poi = projectile;

			// Set the reference to the projectile to null as early as possible
			/*projectile = null;*/

			GameController.ShotFired ();

			powerup=false;

			shootAni = true; 


		}
		
	}

}
