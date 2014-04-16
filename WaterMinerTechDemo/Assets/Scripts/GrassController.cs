using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GrassController : MonoBehaviour {
	private Animator playerAnimator;
	public Sprite[] sprites;
	public float framesPerSecond;

	private SpriteRenderer spriteRenderer;
	private bool touched = false;
	private bool grounded = false;
	private Stopwatch timer;
	//private float groundRadius = 0.2f;
	private GameObject player; // player object for moving 
	private TooBeeController playerController;
	private GameController gameController;

	// Use this for initialization
	void Start () {

		GameObject gameControlObject = GameObject.FindWithTag ("GameController");
		if (gameControlObject != null) {
			gameController = gameControlObject.GetComponent <GameController>(); //get this instance's own game controller connection
		}
		if (gameController == null) {
			UnityEngine.Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		player = GameObject.FindWithTag ("Player");
		if (player != null) {
			playerController = player.GetComponent <TooBeeController>(); //get this instance's own game controller connection
		}

		playerAnimator = player.GetComponent<Animator>();
		timer = new Stopwatch ();
		spriteRenderer = renderer as SpriteRenderer;
		//UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x);
	}
	
	// Update is called once per frame
	void Update () { 


	}

	// FixedUpdate is run at specific time intervals.
	void FixedUpdate () {
		bool dead = gameController.GameOverBool;
		if(! dead){
			if (player.transform.position.x < transform.position.x + (spriteRenderer.bounds.extents.x)
			&& player.transform.position.x > transform.position.x - (spriteRenderer.bounds.extents.x)
			    && player.transform.position.y < transform.position.y + spriteRenderer.bounds.extents.y * 2) {
				grounded = true;
			} else {
				grounded = false;
				touched = false;
				if(timer.IsRunning){
					timer.Stop();
					timer.Reset();
					spriteRenderer.sprite = sprites [0];
				}
			}
		

			if(grounded){
				if (touched) {
					int index = (int)(0.01f * timer.ElapsedMilliseconds);
					//UnityEngine.Debug.Log(boxCol);
					if(index == sprites.Length){
						Destroy(gameObject);
					}else if(index < sprites.Length){
						spriteRenderer.sprite = sprites [index];
					}
				}
			}
		}
	}
	
	void OnMouseDown()
	{	
		bool dead = gameController.GameOverBool;
		
		if (player == null || dead)
			return;

		int stance = playerController.Stance;
		if (player.transform.position.x < transform.position.x + spriteRenderer.bounds.extents.x 
		    && grounded && stance == 1){
			if (playerAnimator != null)
				playerAnimator.Play("Swing");

			if (timer != null)
				timer.Start();

			touched = true;
		}
	}
}