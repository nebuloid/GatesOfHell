using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject cameraTarget; // object to look at or follow
	
	public float smoothTime = 0.1f;    // time for dampen
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 velocity; // speed of camera movement
	
	private Transform cameraTransform; // camera Transform
	private GameController gameController;
	private double screenRatio;
	private const double IDEAL_SCREEN_RATIO = 1.5;
	
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

		screenRatio = (float) Screen.width / (float) Screen.height;
		if (screenRatio > 1.6) {
			ResizeScreenMore();
		}

		if (screenRatio < 1.4) {
			ResizeScreenLess();
		}
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate () {
		bool dead = gameController.GameOverBool;
		if(! dead){
			cameraTransform.position = new Vector3(cameraTransform.position.x, 
		                                      	   Mathf.SmoothDamp(cameraTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime), 
			                                       cameraTransform.position.z);
		}
	}

	void ResizeScreenLess() {
		Camera.main.orthographicSize *= (float) ((IDEAL_SCREEN_RATIO + Mathf.Abs ((float)(screenRatio - IDEAL_SCREEN_RATIO))) / IDEAL_SCREEN_RATIO);
	}

	void ResizeScreenMore() {
		Camera.main.orthographicSize /= (float) ((IDEAL_SCREEN_RATIO + Mathf.Abs ((float)(screenRatio - IDEAL_SCREEN_RATIO))) / IDEAL_SCREEN_RATIO);
	}
}