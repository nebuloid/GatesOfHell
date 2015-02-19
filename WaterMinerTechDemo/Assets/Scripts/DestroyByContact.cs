using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	//public GameObject player;
	public int scoreValue;
	public bool won = false;

	public GUIText livesText;
	public int lives; // move this code into gameController

	private GameController gameController;

	void Start () {
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (won)
			return;
		if (other.tag == "Player") { 
			//player dies // (move this following block of code into gameController.DecrementLives (it will be the majority of that function)
			lives--;
			if(lives==0){
				Debug.Log("Lost all lives");
			} else if (lives > 0) {
				Debug.Log("Lost one life, # of lives left: " + lives);
				livesText.text = "Lives:  " + lives;
			}// end of the section to be moved 

			if (gameController != null) {
				//gameController.DecrementLives ();
				gameController.GameOver ();// move this into decrement lives (if lives == 0) game over
			}

		} else if (other.tag == "Toad") {
			if (gameController != null)
				gameController.AddScore(scoreValue);
		
			//destroy toad
			Destroy (gameObject);
		

			//destroy whatever hit toad
			Destroy (other.gameObject);
		} else {
			gameObject.transform.rigidbody2D.velocity = new Vector2(0,0);
		}
		won = true;
	}
}
