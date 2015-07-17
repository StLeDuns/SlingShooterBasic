using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
	public Text gtScore; // Score GUI Text
	private int counter;
	private int timing;

	void Update(){

		timing++;
		if (timing == 7) {
			if (counter != GameController.Shots) {
				gtScore.text = ": " + counter++;
			} else {
				gtScore.text = ": " + GameController.Shots;
			}
			timing = 0;
		}
	
	}

	public void QuitButtonPressed ()
	{
		Application.Quit ();
	}
	
	public void RestartButtonPressed()
	{
		Application.LoadLevel ("Game");
	}
}
