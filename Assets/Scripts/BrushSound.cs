using UnityEngine;
using System.Collections;

public class BrushSound : MonoBehaviour {

	
	public AudioClip[] audioClip;
	
	void OnTriggerEnter(Collider other) {
		// Check if the hit comes from a projectile
		if (other.gameObject.tag == "Projectile") {
			
			PlaySound (0);		
		}
	}


		void PlaySound(int clip){
			GetComponent<AudioSource>().clip = audioClip[clip];
			GetComponent<AudioSource>().Play ();
		}

		}
