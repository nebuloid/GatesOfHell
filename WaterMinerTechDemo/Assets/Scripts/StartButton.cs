using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public float delay;

	private bool starting = false;
	private float startTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (starting && Time.time > startTime) {
			Application.LoadLevel("level_1");
		}
	}

	void OnMouseDown(){
		starting = true;
		startTime = Time.time + delay;
		audio.Play();
	}
}
