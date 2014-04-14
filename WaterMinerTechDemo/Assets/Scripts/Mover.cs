using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float speed;

	private GameObject player;
	private YoshiControllerScript playerClass;

	void Start ()
	{
		//player = new GameObject ();
		player = GameObject.FindWithTag ("Player");
		playerClass = player.GetComponent<YoshiControllerScript>();
		rigidbody2D.velocity = playerClass.Direction * speed; 
		//Debug.Log (playerClass.Direction); 
		//rigidbody.velocity = transform.forward * speed;
		//rigidbody2D.velocity = YoshiControllerScript.Direction.x * speed;
	}
}
