using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class ToadControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = false;
	public GameObject player;
	public float flipTimer;
	public float stopDistance;

	Animator anim;
	
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	private Stopwatch timer;
	private Animator playerAnimator;
	private float move = 1;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Player");
		playerAnimator = player.GetComponent<Animator>();

		timer = new Stopwatch ();
		timer.Start();

		InvokeRepeating("FlipToad", flipTimer, flipTimer);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//move TOWARDS player
		/*
		if (player.transform.position.x < transform.position.x) {
			move = -1;
		} else {
			move = 1;
		}

		if (Mathf.Abs (player.transform.position.x - transform.position.x) > 2 && grounded) {
			anim.SetFloat ("Speed", Mathf.Abs (move));
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		} else {
			anim.SetFloat ("Speed", 0);
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}
		*/


		if (Mathf.Abs (player.transform.position.x - transform.position.x) > stopDistance) {

			anim.SetFloat ("Speed", 1);
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		} else {
			//player is close so stop moving and face player
			if (player.transform.position.x < transform.position.x) {
				move = -1;
			} else {
				move = 1;
			}

			if (move < 0 && !facingRight)
				Flip ();
			else if (move > 0 && facingRight)
				Flip ();

			anim.SetFloat ("Speed", 0);
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}
	}

	void Update()
	{

	}

	void OnMouseDown()
	{	
		if (player == null)
			return;
		//UnityEngine.Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.rigidbody2D.transform.position.x);
		if (Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.rigidbody2D.transform.position.x) < 2 &&
		    Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.rigidbody2D.transform.position.y) < 2) {
			if (playerAnimator != null)
				playerAnimator.Play("Swing");
		}
	}

	void FlipToad ()
	{
		move = -move;

		if (move < 0 && !facingRight)
			Flip ();
		else if (move > 0 && facingRight)
			Flip ();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
