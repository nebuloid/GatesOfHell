using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//public GUIText scoreText;
	public GUIText _winText;
	public GUIText _gameOverText;
   //public GUIText livesText;
	public AudioClip _deathSound;

	//the three head game objects indicating player lives
	public GameObject _lifeHead1;
	public GameObject _lifeHead2;
	public GameObject _lifeHead3;
    public int _lives;
  
	public string _winString;
	public string _level;

	private bool gameOver;
	private bool won;
	private int score;

    private TooBeeController playerController;

	void Start ()
	{
		gameOver = false;
		won = false;
		_winText.text = "";
		_gameOverText.text = "";
		score = 0;

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
		_winText.text = _winString;
		won = true;
	}

	void UpdateScore (){
		//scoreText.text = "Score: " + score;
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
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

}
