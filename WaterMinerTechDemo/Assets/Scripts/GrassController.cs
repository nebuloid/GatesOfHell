using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GrassController : MonoBehaviour {

	public Sprite[] sprites;
	public float framesPerSecond;
	
	private SpriteRenderer spriteRenderer;
	private bool touched;
	private Stopwatch timer;
	
	// Use this for initialization
	void Start () {
		touched = false;
		timer = new Stopwatch ();
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		if (touched) {
			int index = (int)(0.01f * timer.ElapsedMilliseconds);
			UnityEngine.Debug.Log(index);
			if(index == sprites.Length){
				Destroy(gameObject);
			}else{
				spriteRenderer.sprite = sprites [index];
			}
		}
	}
	
	void OnMouseDown()
	{
		touched = true;
		timer.Start();
	}
}
