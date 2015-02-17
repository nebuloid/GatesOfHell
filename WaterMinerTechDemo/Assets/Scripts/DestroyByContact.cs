using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	//public GameObject player;
	public int scoreValue;
	public bool won = false;
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
			if (gameController != null) {
				gameController.DecrementLives (); // this runs when one collider is a toad and one is the player
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
	}
}
