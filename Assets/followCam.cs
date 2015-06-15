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

		destination.z = camZ;

		transform.position = destination; 
	
	}


}
