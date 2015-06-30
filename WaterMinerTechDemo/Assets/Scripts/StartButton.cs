using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    public float _distance = 0.001f;
    public float _speed = 0.02f;

	private float step = 0f;	
	private bool UP = true;

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
			step += _speed;
		}else{
			step -= _speed;
		}
		
		//Float up and down along the y axis, 
		//Debug.Log(step);
		float tempY = Mathf.Sin(step)+_distance;
		transform.position = new Vector3(transform.position.x,tempY ,transform.position.z);
	}

	void OnMouseDown(){
		GetComponent<AudioSource>().Play();
		loadLevel("level_1");
	}
}
