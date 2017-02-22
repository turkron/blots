using UnityEngine;
using System.Collections;

public class modeText : MonoBehaviour {

	private GameObject GameManager;
	private string gameMode;
	public float Timer = 250;
	private TextMesh textOutput;

	// Use this for initialization
	void Start () {

		GameManager = GameObject.Find("GameManager");
		gameMode = GameManager.GetComponent<GameLoop>().gameMode;
		textOutput = this.gameObject.GetComponent<TextMesh>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Timer == 250){ 
			if(gameMode == "Bonus"){
				textOutput.text = "BONUS!";
			}
			if(gameMode == "Bomb"){
				textOutput.text = "Only use the bomb!";
			}
			if(gameMode == "Chain"){
				textOutput.text = "Link " + GameManager.GetComponent<GameLoop>().chainGoal + " Bubbles together!";
			}
			if(gameMode == "Mass"){
				textOutput.text = "Hit them all!";
			}
			if(gameMode == "Grey"){
				textOutput.text = "Avoid the blue dots!";
			}

		}
		Timer --;
		if(Timer == 0){
			GameObject.Destroy(this.gameObject);
		}
	
	}
}
