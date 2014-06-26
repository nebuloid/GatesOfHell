using UnityEngine;
using System.Collections;

public class TooBeeController : MonoBehaviour {

	public float maxSpeed = 10f;

	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	//public float jumpForce = 0f;
	
	private float groundRadius = 0.2f;
	private bool dead = false;
	//private SpriteRenderer spriteRenderer;

	//constants
	private const int MOVE_STANCE = 1;
	private const int SHOOT_STANCE = 2;

	//move with click
	private Vector3 moveDirection;
	private float moveLocationX;
	private float moveLocationY;
	private bool moving;
	private Vector3 mTargetPoint;

	//toobee shots
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public Vector2 direction;
	public float nextFire;
	public AudioClip shotSound;

	//change stance
	private int stance = 1;
	private int numStances = 2;
	private bool mouseOver;
	
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		//spriteRenderer = renderer as SpriteRenderer;
		mouseOver = false;

		//move with click
		moveDirection = Vector3.right;
		moveLocationX = transform.position.x;
		moving = false;

		//toobee shots
		direction = new Vector2 (0.0f, 0.0f);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);
			
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		if (! mouseOver ) {
			float move = Input.GetAxis ("Horizontal");

			anim.SetFloat ("Speed", Mathf.Abs (move));
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

			if (move > 0 && !facingRight)
					Flip ();
			else if (move < 0 && facingRight)
					Flip ();

			//shoot and move

			switch (stance) {
				case MOVE_STANCE:
				//moving
					MoveMe ();
					break;
				case SHOOT_STANCE:
				//toobee
					Shoot ();
					break;
				default:
				//moving
					MoveMe ();
					break;
			}
		} 
	}

	void Update(){
		//if (grounded && Input.GetKeyDown (KeyCode.Space)) {
		//	anim.SetBool ("Ground",false);
		//	rigidbody2D.AddForce(new Vector2(0, jumpForce));
		//}
	}

	void Shoot(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//make sure mouse is not over player...this wont work for touch

			direction = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
			nextFire = Time.time + fireRate;
			Vector3 spotPosition = new Vector3 (shotSpawn.position.x, shotSpawn.position.y, 0.0f);
			anim.Play("ThrowToobee");
			Instantiate(shot, spotPosition, shotSpawn.rotation);
			audio.clip = shotSound;
			audio.Play();
			
		}
	}

	public void setTargetPoint(Vector3 point) {
		mTargetPoint = point;
		moving = true;
	}

	private void MoveMe(){
		Vector3 currentPosition = transform.position;
		if (moving) {
			Vector3 moveToward = mTargetPoint;
			Debug.Log (mTargetPoint);
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveLocationX = moveToward.x;
			moveLocationY = moveToward.y;
			moveDirection.Normalize ();

			if (moveToward.x > currentPosition.x && !facingRight)
				Flip ();
			else if (moveToward.x < currentPosition.x && facingRight)
				Flip ();

			Vector3 target = moveDirection * maxSpeed + currentPosition;
			transform.position = Vector3.Lerp (currentPosition, target, Time.deltaTime);
			//Debug.Log ("currentPosition.x = " + currentPosition.x + ", moveLocationX = " + moveLocationX);
			if (currentPosition.x < moveLocationX + 0.1f && currentPosition.x > moveLocationX - 0.1f &&
			    currentPosition.y < moveLocationY + 0.1f && currentPosition.y > moveLocationY - 0.1f) {
				moving = false;
			}
		}
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public Vector2 Direction
	{
		get { return direction; }
		set { direction = value; }
	}

	public int Stance
	{
		get { return stance; }
		set { stance = value; }
	}

	void OnMouseOver() {
		mouseOver = true;
	}	

	void OnMouseExit() {
		mouseOver = false;
	}

	void OnMouseDown(){	
		//change stance
		if(stance == numStances){
			stance = MOVE_STANCE;
		}else{
			stance++;
		}
	}

	public bool Dead
	{
		get { return dead; }
		set { dead = value; }
	}

	public void Die(){
		dead = true;
		//Debug.Log("Yoshi Died!!!!!!");
	}
}
