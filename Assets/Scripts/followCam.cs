using UnityEngine;
using System.Collections;

public class followCam : MonoBehaviour {
 
	public static followCam S; 

	public GameObject poi;

	private float camZ;

	void Awake(){
		S = this;
		camZ = this.transform.position.z;
	}

	void Update(){
	
		if (poi == null) {
			return;
		}

		Vector3 destination = poi.transform.position;

		//Limit the x&y position to minimum values
		destination.x = Mathf.Max(0,destination.x);
		destination.y= Mathf.Max (0,destination.y);

		destination.z = camZ;

		transform.position = destination; 

		this.GetComponent<Camera> ().orthographicSize = 10 + destination.y;
	
	}


}
