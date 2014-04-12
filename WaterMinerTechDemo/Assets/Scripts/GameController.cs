using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText scoreText;
	public GUIText winText;
	public GUIText gameOverText;

	public GameObject player;
	public float winDepth;
	public string winString;
	public string level;

	private bool gameOver;
	private bool won;
	private int score;

	void Start ()
	{
		gameOver = false;
		won = false;
		winText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		//StartCoroutine (SpawnWaves ());
	}

	void Update (){

	}
	
	void FixedUpdate () {
		if (player.transform.position.y < winDepth && !won) {
			Victory ();
		}
		
		if (Input.GetButton ("Fire1") && won) {
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

	public void GameOver (){
		gameOverText.text = "Game Over Man!";
		gameOver = true;
	}

}
