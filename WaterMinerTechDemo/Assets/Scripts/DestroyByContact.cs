using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
/*
	public GameObject explosion;
	public GameObject playerExplosion;
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

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}

		Instantiate(explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		gameController.AddScore (scoreValue);

		//destroy whatever asteroid collided with
		Destroy (other.gameObject);

		//destroy this asteroid
		Destroy (gameObject);
	}*/
}
