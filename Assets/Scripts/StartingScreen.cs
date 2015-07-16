using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartingScreen : MonoBehaviour
{

	public Image title;
	float fade;
		 
	void Update ()
	{
		fade += 0.01f;

		title.color = new Color (1f, 1f, 1f, fade);

		if (fade == 1) {
			fade = 1;
		}
	}

	public void StartButtonPressed ()
	{
		Application.LoadLevel ("Game");
	}

	public void QuitButtonPressed ()
	{
		Application.Quit ();
	}
}
