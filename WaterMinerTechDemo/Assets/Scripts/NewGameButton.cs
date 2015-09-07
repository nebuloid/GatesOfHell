using UnityEngine;
using System.Collections;

public class NewGameButton : MonoBehaviour {
	
	private string _sceneID = "level_1";
	
	// Use this for initialization
	void Start () {
		//Store where we were placed in the editor
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		PlayerPrefs.DeleteAll ();
		Application.LoadLevel(_sceneID);
	}
}
