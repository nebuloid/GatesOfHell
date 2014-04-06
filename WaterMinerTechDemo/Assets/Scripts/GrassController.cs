using UnityEngine;
using System.Collections;

public class GrassController : MonoBehaviour {

	private Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown()
	{
		animator.SetInteger("Dig", 1);
		//animator.speed = 1.0f;
		//animator.Play();
		Destroy(gameObject);
	}
}
