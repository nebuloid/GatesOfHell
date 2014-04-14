using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject cameraTarget; // object to look at or follow
	
	public float smoothTime = 0.1f;    // time for dampen
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 velocity; // speed of camera movement
	
	private Transform cameraTransform; // camera Transform
	private GameController gameController;
	
	// Use this for initialization
	void Start()
	{
		cameraTransform = transform;
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate () {
		bool dead = gameController.GameOverBool;
		if(! dead){
			cameraTransform.position = new Vector3(Mathf.SmoothDamp(cameraTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime), 
		                                      	   Mathf.SmoothDamp(cameraTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime), 
																	cameraTransform.position.z);
		}
	}
}