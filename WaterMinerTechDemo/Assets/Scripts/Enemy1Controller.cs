using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Enemy1Controller : MonoBehaviour {

	public float maxSpeedX = 10f;
	public float maxSpeedY = 10f;
	bool facingRight = false;
	bool facingUp = false;
	public GameObject playerObject;
	public float flipTimerX = 10f;
	public float flipTimerY = 10f;

	Animator anim;
	
	public Transform groundCheck;
	public LayerMask whatIsGround;
	private Stopwatch timer;
	private Animator playerAnimator;
	private float moveX = 1;
	private float moveY = 1;
	private GameController gameController;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		playerObject = GameObject.FindWithTag ("Player");
		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		playerAnimator = playerObject.GetComponent<Animator>();

		timer = new Stopwatch ();
		timer.Start();

		InvokeRepeating("FlipToadX", flipTimerX, flipTimerX);
		InvokeRepeating("FlipToadY", flipTimerY, flipTimerY);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool dead = gameController.GameOverBool;
		if(! dead){
			anim.SetFloat ("Speed", 1);
			rigidbody2D.velocity = new Vector2 (moveX * maxSpeedX, moveY * maxSpeedY);
		}
	}

	void Update()
	{

	}

	void OnMouseDown()
	{	
		if (playerObject == null)
			return;
		//UnityEngine.Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.rigidbody2D.transform.position.x);
		if (Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - playerObject.rigidbody2D.transform.position.x) < 2 &&
		    Mathf.Abs (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - playerObject.rigidbody2D.transform.position.y) < 2) {
			if (playerAnimator != null)
				playerAnimator.Play("Swing");
		}
	}

	void FlipToadX ()
	{
		moveX = -moveX;
		if (moveX < 0 && !facingRight)
			FlipX ();
		else if (moveX > 0 && facingRight)
			FlipX ();
	}

	void FlipToadY ()
	{
		moveY = -moveY;
	}

	void FlipX()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
