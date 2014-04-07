using UnityEngine;
using System.Collections;

public class GameConditions : MonoBehaviour {

	public float winDepth;

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		foreach (Transform child in transform)
		{
			Debug.Log("Found a child");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (transform.position.y < winDepth) {
			Reset();
		}
	}

	void Reset() {
		Application.LoadLevel (0);
		//transform.position = originalPosition;
		//transform.rotation = originalRotation;
		/*
		if (rigidbody != null) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}*/
	}
}
