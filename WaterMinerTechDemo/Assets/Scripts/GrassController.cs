using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GrassController : MonoBehaviour {

	public Sprite[] sprites;
	public float framesPerSecond;

	private SpriteRenderer spriteRenderer;
	private bool touched;
	private Stopwatch timer;

	private GameObject player; // player object for moving 
	
	// Use this for initialization
	void Start () {

		player = GameObject.FindWithTag("Player");
		touched = false;
		timer = new Stopwatch ();
		spriteRenderer = renderer as SpriteRenderer;
		UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x);
	}
	
	// Update is called once per frame
	void Update () { 

		if(player.transform.position.x < transform.position.x + (spriteRenderer.bounds.extents.x)
		   && player.transform.position.x > transform.position.x - (spriteRenderer.bounds.extents.x)){
			if (touched) {
				int index = (int)(0.01f * timer.ElapsedMilliseconds);
				//UnityEngine.Debug.Log(boxCol);
				if(index == sprites.Length){
					Destroy(gameObject);
				}else{
					spriteRenderer.sprite = sprites [index];
				}
			}
		}
	}
	
	void OnMouseDown()
	{
		touched = true;
		if (player.transform.position.x < transform.position.x + (spriteRenderer.bounds.extents.x)
		&& player.transform.position.x > transform.position.x - (spriteRenderer.bounds.extents.x)) {
			timer.Start();
		}
	}
}
