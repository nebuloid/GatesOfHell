using UnityEngine;
using System.Collections;

public class ToadSpawner : MonoBehaviour {

	public float delay;
	public GameObject toad;

	//private bool starting = false;
	private float startTime = 0f;
	private GameController gameController;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time + delay;

		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}
	
	// Update is called once per frame
	void Update () {
		bool dead = gameController.GameOverBool;
		if(! dead){
			if (Time.time > startTime) {
				Instantiate(toad, transform.position, transform.rotation);
				startTime = Time.time + delay;
				audio.Play();
			}
		}
	}
	
}
