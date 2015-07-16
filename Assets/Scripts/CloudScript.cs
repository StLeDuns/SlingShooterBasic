using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {


	public int numClouds = 40;

	public Vector3 cloudPosMin;
	public Vector3 cloudPosMax;

	public GameObject[] cloudPrefabs;

	//private fields
		private GameObject[] cloudInstances;

	void Awake(){

		//Make an array large enough to hold all clouds
		cloudInstances = new GameObject [numClouds];

		//Find the Cloud Parent Object
		GameObject cloudParent = GameObject.Find ("Clouds");

		//Iterate and generate each cloud in a loop
		for (int i=0; i<numClouds; i++) {

			// Randomly choose one of the prefabs
			int chosenPrefab = Random.Range (0, cloudPrefabs.Length);
			//Instatiate it
			GameObject cloud = Instantiate (cloudPrefabs [chosenPrefab]);

			//Set a random position and scale
			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range (cloudPosMin.x, cloudPosMax.x);
			cPos.y = Random.Range (cloudPosMin.y, cloudPosMax.y);

			//Make the cloud a child of out Parent Object
			cloud.transform.parent = cloudParent.transform;
			//Add the new cloud to our instance array
			cloudInstances[i] = cloud;
		}

	}
}