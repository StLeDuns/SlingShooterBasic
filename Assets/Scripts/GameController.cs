﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
	idle,
	playing,
	levelEnd
}

public class GameController : MonoBehaviour
{

	public AudioClip[] audioClip;
	public static GameController S; // Singleton

	// Fields set in Unity Inspector pane
	public GameObject[] castles; // An array with all castles
	public Text gtLevel; // Level GUI Text
	public Text gtScore; // Score GUI Text
	public Vector3 castlePos; // Place to put castles

	// Dynamic fields
	public int level; // Current level
	public int levelMax; // Number of levels
	public int shotsTaken;
	public GameObject castle; // The current castle
	public GameState state = GameState.idle;
	public string showing = "Slingshot"; // FollowCam mode

	public static int Shots;
	
	void Start ()
	{
		S = this;
		level = 0;
		levelMax = castles.Length;
		StartLevel ();
	}

	void StartLevel ()
	{
		// If a castle exists, get rid of it
		if (castle != null) {
			Destroy (castle);
		}

		// Destroy the old projectiles
		GameObject[] projectiles = GameObject.FindGameObjectsWithTag ("Projectile");
		foreach (GameObject p in projectiles) {
			Destroy (p);
		}

		// Instantiate the new castle
		castle = Instantiate (castles [level]) as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		// Reset the camera
		SwitchView ("Both");
		ProjectileLine.S.Clear ();

		// Reset the Goal
		Goal.goalMet = false;
		UpdateGT ();

		state = GameState.playing;
	
	}

	void UpdateGT ()
	{
		gtLevel.text = ": " + (level + 1) + " of " + levelMax;
		gtScore.text = ": " + shotsTaken;
		Shots=shotsTaken;

	}

	void Update ()
	{
		UpdateGT ();

		// Check for level end
		if (state == GameState.playing && Goal.goalMet) {
			if (FollowCam.S.poi.tag == "Projectile" && FollowCam.S.poi.GetComponent<Rigidbody> ().IsSleeping ()) {
				// Change state to stop checking for level end
				state = GameState.levelEnd;

				// Start next level in 2 seconds
				Invoke ("NextLevel", 0.5f);
				SwitchView ("Both");
			}
		}
	}

	void NextLevel ()
	{
		level++;

		PlaySound (0);


		if (level == levelMax) {

			Application.LoadLevel ("EndScreen");
		}
		StartLevel ();


	}

	// Static to change the view point
	public void SwitchView (string view)
	{
		S.showing = view;
		switch (S.showing) {
		case "Slingshot":
			FollowCam.S.poi = null;
			break;
		case "Castle":
			FollowCam.S.poi = S.castle;
			break;
		case "Both":
			FollowCam.S.poi = GameObject.Find ("ViewBoth");
			break;
		}
	}

	// Static function that allows to increment the score
	public static void ShotFired ()
	{
		S.shotsTaken++;

	}
	
	void PlaySound (int clip)
	{
		GetComponent<AudioSource> ().clip = audioClip [clip];
		GetComponent<AudioSource> ().Play ();
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
