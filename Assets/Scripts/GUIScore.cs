using UnityEngine;
using System.Collections;

public class GUIScore : MonoBehaviour {
	private float currentScore = -1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(currentScore != GameLoop.playerScore){
			this.GetComponent<TextMesh>().text = "Score: " + GameLoop.playerScore;
			currentScore = GameLoop.playerScore;
		}

	}
}
