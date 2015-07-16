using UnityEngine;
using System.Collections;

public class ToothpasteAnimation : MonoBehaviour {
	
	public AudioClip[] audioClip;

void Update(){
		if (Slingshot.loadAni == true) {
			GetComponent<Animation> ().Play ("Load");	
			Slingshot.loadAni = false;

			PlaySound (1);
		}

		if (Slingshot.shootAni == true) {
			
			GetComponent<Animation> ().Play ("Shoot");
			Slingshot.shootAni = false;

			PlaySound (0);
		}
	
	}

void PlaySound(int clip){
	GetComponent<AudioSource>().clip = audioClip[clip];
	GetComponent<AudioSource>().Play ();
}

}