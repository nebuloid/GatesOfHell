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

	// Use this for initialization
	void Start () {

		player = GameObject.FindWithTag ("Player");
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
	
	void OnMouseDown()
	{	
		if (player == null)
			return;
		if (player.transform.position.x < transform.position.x + spriteRenderer.bounds.extents.x && grounded){
			if (playerAnimator != null)
				playerAnimator.Play("Swing");

			if (timer != null)
				timer.Start();

			touched = true;
		}
	}
}