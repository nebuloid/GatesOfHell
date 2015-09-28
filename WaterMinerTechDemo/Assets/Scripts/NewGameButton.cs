using UnityEngine;
using System.Collections;

public class NewGameButton : MonoBehaviour {
	
	private string _sceneID = "level_1";
	
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("totalScore", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		PlayerPrefs.DeleteAll ();
		Application.LoadLevel(_sceneID);
	}
}
