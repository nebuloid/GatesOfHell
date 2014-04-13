using UnityEngine;
using System.Collections;

public class ToadSpawner : MonoBehaviour {

	public float delay;
	public GameObject toad;

	private bool starting = false;
	private float startTime = 0f;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime) {
			Instantiate(toad, transform.position, transform.rotation);
			startTime = Time.time + delay;
			audio.Play();
		}
	}
	
}
