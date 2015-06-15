using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	//Fields seen in the inspector panel
	public GameObject prefabProjectile;
	public float shotMult = 0.4f;

	//Internal variable
	private GameObject launchPoint;
	private Vector3 launchPos;
	private GameObject projectile;

	bool aimingMode;

	void Awake(){
		Transform launchPointTransform = transform.Find ("LaunchPoint");
		launchPoint = launchPointTransform.gameObject;
		launchPoint.SetActive (false);

	
		launchPos = launchPointTransform.position;
	
	}


	void OnMouseEnter(){
		//print ("pups");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		launchPoint.SetActive (false);
		//print ("backe");
	}

	void OnMouseDown(){
		aimingMode = true;

		//Instantiate a new projectile 
		projectile = Instantiate(prefabProjectile) as GameObject;

		//Start it at the launchpoint
		//Set the projectile's position t the launchPos
		projectile.transform.position = launchPos;
	
		//Set isKinematic to true or false
		projectile.GetComponent<Rigidbody> ().isKinematic = true;
	
	}


	void Update(){
		//if we're not in aiming mode, do nothing
		if (!aimingMode) { 
			return;
		}
		
		//Get the current mouse position in 2D
		Vector3 mousePos2D = Input.mousePosition;
		
		//Convert it to 3D World coordinates
		mousePos2D.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
		
		//Find the difference between the launchPos and mouse position 
		Vector3 mouseDelta = mousePos3D - launchPos;

		float maxMagnitude = this.GetComponent<SphereCollider> ().radius; 

		mouseDelta = Vector3.ClampMagnitude (mouseDelta, maxMagnitude);

		//Move the projectile to this new position 
		projectile.transform.position = launchPos + mouseDelta; 

		if (Input.GetMouseButtonUp (0)) {
			aimingMode = false;
			projectile.GetComponent<Rigidbody> ().isKinematic = false;

			projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * shotMult;

			followCam.S.poi =projectile;
		}

	}

	}