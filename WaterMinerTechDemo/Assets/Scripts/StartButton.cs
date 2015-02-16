using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public float delay;

	//private bool starting = false;
	//private float startTime = 0f;
	private float step = 0f;
	public float distance;
	private bool UP = true;
	public float speed;


	// Use this for initialization
	void Start () {
		//Store where we were placed in the editor
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public static void loadLevel(string sceneName)
	{
		//LoadingScreen.show();
		Application.LoadLevel(sceneName);
	}

	void FixedUpdate () {
		
		//Make sure Steps value never gets too out of hand 
		if(step >= 0.3f){
			UP = true;
		}else if(step <= 0f){
			UP = false;
		}
		
		if(UP){
			step += speed;
		}else{
			step -= speed;
		}
		
		//Float up and down along the y axis, 
		//Debug.Log(step);
		float tempY = Mathf.Sin(step)+distance;
		transform.position = new Vector3(transform.position.x,tempY ,transform.position.z);
	}

	void OnMouseDown(){
		audio.Play();
		loadLevel("level_1");
	}
}
