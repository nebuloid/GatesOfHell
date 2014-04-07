using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject cameraTarget; // object to look at or follow
	//public GameObject player; // player object for moving
	
	public float smoothTime = 0.1f;    // time for dampen
	public bool cameraFollowX = true; // camera follows on horizontal
	public bool cameraFollowY = true; // camera follows on vertical
	public bool cameraFollowHeight = true; // camera follow CameraTarget object height
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 velocity; // speed of camera movement
	
	private Transform cameraTransform; // camera Transform
	
	// Use this for initialization
	void Start()
	{
		cameraTransform = transform;
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate () {
		cameraTransform.position = new Vector3(Mathf.SmoothDamp(cameraTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime), 
		                                       Mathf.SmoothDamp(cameraTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime), 
																cameraTransform.position.z);
	}
}