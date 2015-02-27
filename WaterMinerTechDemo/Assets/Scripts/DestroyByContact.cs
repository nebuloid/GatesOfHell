using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	//public GameObject player;
	public int _scoreValue;
	private bool mWon = false;
    private GameController mGameController;

	void Start () {
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			mGameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (mGameController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (mWon)
			return;
		if (other.tag == "Player") { 
			if (mGameController != null) {
				mGameController.DecrementLives (); // this runs when one collider is a toad and one is the player
			}
		} else if (other.tag == "Toad") {
			if (mGameController != null)
				mGameController.AddScore(_scoreValue);
		
			//destroy toad
			Destroy (gameObject);
			//destroy whatever hit toad
			Destroy (other.gameObject);
		} else {
			gameObject.transform.rigidbody2D.velocity = new Vector2(0,0);
		}
	}
}
