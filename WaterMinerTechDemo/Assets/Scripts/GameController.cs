﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour {

	public AudioClip _deathSound;
	public AudioClip deathSound;

	public Text scoreText;
	//public static Timer scoreTimer;

	//the three head game objects indicating player lives
	public GameObject _lifeHead1;
	public GameObject _lifeHead2;
	public GameObject _lifeHead3;

    public int _lives;
	private float _scoreFloat;
	private int _scoreInt;
	private int mHighScore;
	private float mInvulnerabilityCountDown;

	//public string _winString;
	public string _level;

	private bool gameOver;
	private bool won;
	//this is turned to true when the level is completed
	private bool isWindowShown;
	private bool isInvulnerable;

	public Rect windowRect = new Rect(Screen.width/2, Screen.height/2, 120, 90);

    private TooBeeController playerController;
	private InvulnerabilityColision invulnerabilityColision;

	void Start ()
	{
		gameOver = false;
		won = false;
		isWindowShown = false;
		Load ();
		_scoreFloat = 1000;
		mInvulnerabilityCountDown = 30;
		scoreText.text = "Score: " + _scoreFloat;

		GameObject playerObject = GameObject.FindWithTag ("Player");
		GameObject invulnerabilityObject = GameObject.FindWithTag("Invulnerability");
		if (playerObject != null) {
			playerController = playerObject.GetComponent <TooBeeController>(); //get this instance's own game controller connection
		}
		if (playerController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		//trying to connect invulnerabilityColision to this class
		if (invulnerabilityObject != null) {
			invulnerabilityColision = invulnerabilityObject.GetComponent <InvulnerabilityColision>(); //get this instance's own game controller connection

		}
		if (invulnerabilityColision == null) {
			Debug.Log("Cannot find 'invulnerability' script"); //logging in case unable to find gamecontroller
		}

		UpdateScore();

	}

	void Update (){
		isInvulnerable = invulnerabilityColision.getIsInvulnerable();
		//if the level isn't finished then decrement the score
		if (!isWindowShown) {
			DecrementScore();
		}
		if(gameOver && audio.loop){
			audio.loop = false;
		}
		if(isInvulnerable) {
			InvulnerablilityCountdown();
		}
	}
	
    void FixedUpdate () {

        if (Input.GetButton ("Fire1") && gameOver) {
            Application.LoadLevel ("menu"); // loads a new level (right now it is set to load the same over and over
        }

        if (Input.GetButton ("Fire1") && won) {
            Application.LoadLevel (_level); // loads a new level (right now it is set to load the same over and over
        }
    }
	
	public void Victory() {
		Save ();
		isWindowShown = true;
		/* 
		 * won equals true is now called 
		 * if the user clicks the 'next level'
		 * button in the gui text box
		 */
		//won = true;
	}

	void UpdateScore (){
		scoreText.text = "Score: " + _scoreInt;
	}

	public void AddScore (int newScoreValue){
		_scoreFloat += newScoreValue;
		UpdateScore();
	}

	public bool GameOverBool
	{
        get { return gameOver; }
		set { gameOver = value; }
	}

	public void DecrementLives ()
	{
        _lives--;
        if(_lives==0){
            GameOver ();
			removeHeads (_lives);
        } else if (_lives > 0) {

			removeHeads (_lives); 
            audio.clip = _deathSound;
            audio.Play();
            playerController.Die(); // moves player to starting point
        }
	}

	/*
	 * This is the method that is called in Update();
	 * I use Time.deltaTime * 6 just beacuse it seems 
	 * like a good speed to decrement at.
	 */
	public void DecrementScore() {
		_scoreFloat -= Time.deltaTime * 6;
		_scoreInt = (int) _scoreFloat;
		if (_scoreFloat < 0) {
			_scoreFloat = 0;
		}
		scoreText.text = "Score: " + _scoreInt;
	}

    private void GameOver (){
        gameOver = true;
        audio.clip = _deathSound;
        audio.Play();
        // this line below here
        Application.LoadLevel ("menu"); // loads a new level (right now it is set to load the same over and over
        ; // prepares the level that will be loaded when player clicks
        //gameOverText.text = "Game Over Man!";
    }


	/*
	 * Called in decrementLives()
	 * this method takes in how many lives the
	 * user currently has and then removes the 
	 * TooBee head from the screen to match it.
	 */
	private void removeHeads(int lives) {
		switch (lives) {
			case 2:
				Destroy (_lifeHead3);
				break;
			case 1:
				Destroy (_lifeHead2);
				break;
			case 0:
				Destroy (_lifeHead1);
				break;
		}
	}

	private void InvulnerablilityCountdown() {
		mInvulnerabilityCountDown -= Time.deltaTime;
		if(mInvulnerabilityCountDown <= 0) {
			Debug.Log("Invulnerable time is finished");
			isInvulnerable = false;
			invulnerabilityColision.setIsInvulnerable(isInvulnerable);
			mInvulnerabilityCountDown = 30;
		}
		Debug.Log ("Time is Ticking away, " + mInvulnerabilityCountDown);
	}

	/*
	 *This function controls the pop up window that displays when
	 *the level has been beaten.
	 */
	private void OnGUI() {
		if(isWindowShown){
			windowRect = GUI.Window(0, windowRect, DoMyWindow, "Level Complete");
		}

	}

	/*
	 * This function adds two text fields and a button to the pop up window.
	 * If the button is clicked it sends the user to the next level.
	 */ 
	private void DoMyWindow(int windowID) {
		GUI.TextField(new Rect(10,40,100,20),"Score: " + _scoreInt);
		GUI.TextField(new Rect(10, 60, 100,20), "High Score: " + mHighScore);
		if (GUI.Button(new Rect(10, 20, 100, 20), "Next Level")) {
			won = true;
			//print("next level, Score: " + _scoreInt);
		}
	}

	/*
	 * This funciton will run when the level is beaten.
	 * It saves the highScore if the current score is 
	 * higher than it.
	 */
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/highScore.dat");

		HighScoreData highScoreData = new HighScoreData();

		if (mHighScore < _scoreInt) {
			Debug.Log("new High Score!" + _scoreInt);
			mHighScore = _scoreInt;
			highScoreData.highScore = _scoreInt;
			
		} else {
			Debug.Log("no new highScore");
		}

		bf.Serialize(file,highScoreData);
		file.Close();
	}

	/*
	 * This is ran in the on Start function
	 * it loads the highscore from a file
	 */
	public void Load() {
		if (File.Exists(Application.persistentDataPath + "/highScore.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat",FileMode.Open);
			HighScoreData data = (HighScoreData)bf.Deserialize(file);
			file.Close ();
			mHighScore = data.highScore;
			Debug.Log("highscore"  + mHighScore);
		}
	}

}

/*
 * private class that implments seralizable
 * it stores the highScore.
 */
	[Serializable]
class HighScoreData {
	public int highScore;
}
