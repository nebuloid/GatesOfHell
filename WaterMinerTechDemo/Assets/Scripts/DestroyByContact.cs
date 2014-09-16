using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	//public GameObject player;
	public int scoreValue;

	private GameController gameController;

	void Start (){
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "Player") { 
			//player dies
			if (gameController != null)
				gameController.GameOver ();
			
		}else{
			if (gameController != null)
				gameController.AddScore(scoreValue);
		
			//destroy toad
			Destroy (gameObject);
		}

		//destroy whatever hit toad
		Destroy (other.gameObject);
	}
}
