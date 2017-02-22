using UnityEngine;
using System.Collections;

public class infoText : MonoBehaviour {

	private GameObject GameManager;
	private int inputText;
	private TextMesh textOutput;
	private bool calculatedPerformace = false;
	public int Timer;
	static public string howWellDidYouDo;
	public string originalGameMode;
	public float ChainGreatBonus;
	public float MassGreatBonus;
	public float MassOkayBonus;
	public float MassPoorBonus;
	public float GreyGreatBonus;
	public float GreyOkayBonus;
	public float GreyPoorBonus;

	// Use this for initialization
	void Start () {
		GameManager = GameObject.Find("GameManager");
		textOutput = this.gameObject.GetComponent<TextMesh>();
		originalGameMode = GameManager.GetComponent<GameLoop>().gameMode;
	}
	
	// Update is called once per frame
	void Update () {
	


		if(GameManager.GetComponent<GameLoop>().gameMode == "Chain"){
			textOutput.text = "" + GameLoop.frozenDotCount;
		}




		if(GameManager.GetComponent<GameLoop>().gameMode == "Normal"){

			Timer --;

			if(!calculatedPerformace){
				calculatedPerformace = CalculatePerformance(originalGameMode,howWellDidYouDo);
			}

			if(Timer < 0){
				howWellDidYouDo = "";
				GameObject.Destroy(this.gameObject);
			} 
		}

	}

	bool CalculatePerformance(string originalMode, string rating){

		if(originalMode == "Chain"){
			if(rating == "Great"){
				textOutput.text = "Awesome! +" + ChainGreatBonus;
				GameLoop.playerScore += ChainGreatBonus;
			}

		}
		if(originalMode == "Mass"){
			if(rating == "Great"){
				textOutput.text = "Blot on! +" + MassGreatBonus;
				GameLoop.playerScore += MassGreatBonus;
			}

			if(rating == "Okay"){
				textOutput.text = "Just a few more! +" + MassOkayBonus;
				GameLoop.playerScore += MassOkayBonus;
			}

			if(rating == "Poor"){
				textOutput.text = "You can do it! +" + MassPoorBonus;
				GameLoop.playerScore += MassPoorBonus;
			}
			
		}

		if(rating == "Failed"){
			int randomText = Random.Range(1,4);
			if(randomText == 1)
				textOutput.text = "Unlucky...";
			if(randomText == 2)
				textOutput.text = "Try again...";
			if(randomText == 3)
				textOutput.text = "Better luck next time...";
			if(randomText == 4)
				textOutput.text = "All blotted out?...";
		}

		return true;
	}
}
