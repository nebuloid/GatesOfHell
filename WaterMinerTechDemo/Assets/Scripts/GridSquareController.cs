using UnityEngine;
using System.Collections;

public class GridSquareController : MonoBehaviour {
	public Sprite[] sprites;
	public float framesPerSecond;
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

		//UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x);
		BoxCollider2D box = (BoxCollider2D) player.GetComponent("BoxCollider2D");
		CircleCollider2D circle = (CircleCollider2D) player.GetComponent("CircleCollider2D");

		Physics2D.IgnoreCollision(box, transform.collider2D);
		Physics2D.IgnoreCollision(circle, transform.collider2D);
	}
	
	void OnMouseDown(){
		//UnityEngine.Debug.Log("player = "+ player.transform.position);
		//UnityEngine.Debug.Log(Vector2.Distance(player.transform.position, transform.position));
		//UnityEngine.Debug.Log(spriteRenderer.bounds.extents.x * 2);
		
		bool dead = gameController.GameOverBool;
		
		if (player == null || dead)
			return;
		
		int stance = playerController.Stance;
		if (stance == 1) {
			playerController.setTargetPoint(transform.position);
		}
	}
}
