using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	public GameObject launchPoint;

	void Awake(){
		Transform launchPointTransform = transform.Find ("LaunchPoint");
		launchPoint = launchPointTransform.gameObject;
		launchPoint.SetActive (false);
		}


	void OnMouseEnter(){
		//print ("pups");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		launchPoint.SetActive (false);
		//print ("backe");
	}

	}