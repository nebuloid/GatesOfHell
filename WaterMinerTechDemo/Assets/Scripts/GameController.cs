using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Timers;

public class GameController : MonoBehaviour {

	public AudioClip _deathSound;
	public AudioClip deathSound;

	public Text scoreText;
	//public static Timer scoreTimer;

	//the three head game objects indicating player lives
	public GameObject _lifeHead1;
	public GameObject _lifeHead2;
	public GameObject _lifeHead3;
    public int _lives;
	private float _scoreFloat;
	private int _scoreInt;
	//public string _winString;
	public string _level;

	private bool gameOver;
	private bool won;


    private TooBeeController playerController;

	void Start ()
	{
		gameOver = false;
		won = false;

		_scoreFloat = 1000;
		scoreText.text = "Score: " + _scoreFloat;

		/*scoreTimer = new Timer(1000);
		scoreTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		scoreTimer.Enabled = true;*/
			
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) {
			playerController = playerObject.GetComponent <TooBeeController>(); //get this instance's own game controller connection
		}
		if (playerController == null) {
			Debug.Log("Cannot find 'GameController' script"); //logging in case unable to find gamecontroller
		}

		UpdateScore();

	}

	void Update (){

		DecrementScore();
		if(gameOver && audio.loop){
			audio.loop = false;
		}
	}
	
    void FixedUpdate () {

        if (Input.GetButton ("Fire1") && gameOver) {
            Application.LoadLevel ("menu"); // loads a new level (right now it is set to load the same over and over
        }

        if (Input.GetButton ("Fire1") && won) {
            Application.LoadLevel (_level); // loads a new level (right now it is set to load the same over and over
        }
    }
	
	public void Victory() {
		won = true;
	}

	void UpdateScore (){
		scoreText.text = "Score: " + _scoreInt;
	}

	public void AddScore (int newScoreValue){
		_scoreInt += newScoreValue;
		UpdateScore();
	}

	public bool GameOverBool
	{
        get { return gameOver; }
		set { gameOver = value; }
	}

	public void DecrementLives ()
	{
        _lives--;
        if(_lives==0){
            GameOver ();
			removeHeads (_lives);
        } else if (_lives > 0) {

			removeHeads (_lives); 
            audio.clip = _deathSound;
            audio.Play();
            playerController.Die(); // moves player to starting point
        }
	}

	/*
	 * This is the method that is called in Update();
	 * I use Time.deltaTime * 6 just beacuse it seems 
	 * like a good speed to decrement at.
	 */
	public void DecrementScore() {
		_scoreFloat -= Time.deltaTime * 6;
		_scoreInt = (int) _scoreFloat;
		if (_scoreFloat < 0) {
			_scoreFloat = 0;
		}
		scoreText.text = "Score: " + _scoreInt;
	}

    private void GameOver (){
        gameOver = true;
        audio.clip = _deathSound;
        audio.Play();
        // this line below here
        Application.LoadLevel ("menu"); // loads a new level (right now it is set to load the same over and over
        ; // prepares the level that will be loaded when player clicks
        //gameOverText.text = "Game Over Man!";
    }


	/*
	 * Called in decrementLives()
	 * this method takes in how many lives the
	 * user currently has and then removes the 
	 * TooBee head from the screen to match it.
	 */
	private void removeHeads(int lives) {
		switch (lives) {
			case 2:
				Destroy (_lifeHead3);
				break;
			case 1:
				Destroy (_lifeHead2);
				break;
			case 0:
				Destroy (_lifeHead1);
				break;
		}
	}
	/*
	public void OnTimedEvent(object scource, ElapsedEventArgs e) {
		DecrementScore();
	}*/

}
