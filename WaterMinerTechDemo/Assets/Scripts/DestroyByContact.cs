using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	public GameObject player;
	public int scoreValue;

	private GameController gameController;

	void Start (){
		//GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		//if (gameControlObject != null) {
			//gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		//}
		//if (gameController == null) {
			//Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		//}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "TooBee") {
			gameController.AddScore (scoreValue);
			//toad dies
		}

		if (other.tag == "Player") {
			//player
			//player dies
			gameController.GameOver ();
		}

		//destroy whatever hit toad
		Destroy (other.gameObject);

		//destroy toad
		Destroy (gameObject);
	}
}
