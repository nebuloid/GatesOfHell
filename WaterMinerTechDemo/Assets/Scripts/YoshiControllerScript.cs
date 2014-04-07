using UnityEngine;
using System.Collections;

public class YoshiControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	//move with click
	private Vector3 moveDirection;
	private float moveLocationX;
	private bool moving;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

		//move with click
		moveDirection = Vector3.right;
		moveLocationX = transform.position.x;
		moving = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground",false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		//move with click
	
		Vector3 currentPosition = transform.position;

		if( Input.GetButton("Fire1") ) {

			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			//Debug.Log (Input.mousePosition);
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.y = 0;
			moveLocationX = moveToward.x;
			moveDirection.Normalize();
			moving = true;
			if (moveToward.x > currentPosition.x && !facingRight)
				Flip ();
			else if (moveToward.x < currentPosition.x && facingRight)
				Flip ();
		}

		if (moving) {
			Vector3 target = moveDirection * maxSpeed + currentPosition;
			transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
			//Debug.Log ("currentPosition.x = " + currentPosition.x + ", moveLocationX = " + moveLocationX);
			if(currentPosition.x < moveLocationX + 0.1f && currentPosition.x > moveLocationX - 0.1f){
				moving = false;
			}
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
