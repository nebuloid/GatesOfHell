using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText scoreText;
	public GUIText winText;
	public GUIText gameOverText;
	public AudioClip deathSound;

	public GameObject player;
	public float winDepth;
	public string winString;
	public string level;

	private bool gameOver;
	private bool won;
	private int score;
	private YoshiControllerScript playerController;

	void Start ()
	{
		gameOver = false;
		won = false;
		winText.text = "";
		gameOverText.text = "";
		score = 0;

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) {
			playerController = playerObject.GetComponent <YoshiControllerScript>(); //get this instance's own game controller connection
		}
		if (playerController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		UpdateScore();
	}

	void Update (){
		if(gameOver && audio.loop){
			audio.loop = false;
		}	
	}
	
	void FixedUpdate () {
		
		if (player.transform.position.y < winDepth && ! won && ! gameOver) {
			Victory ();
		}
		
		if (Input.GetButton ("Fire1") && (won || gameOver)) {
			Application.LoadLevel (level);
		}
	}
	
	void Victory() {
		winText.text = winString;
		won = true;
	}

	void UpdateScore (){
		scoreText.text = "Score: " + score;
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore();
	}

	public bool GameOverBool
	{
		get { return gameOver; }
		set { gameOver = value; }
	}

	public void GameOver (){
		gameOver = true;
		audio.clip = deathSound;
		audio.Play();
		playerController.Die();
		level = Application.loadedLevelName;
		gameOverText.text = "Game Over Man!";
	}

}
