using UnityEngine;
using System.Collections;

public class BonusBar : MonoBehaviour {

	private float maxSize;
	public float growPercentage;
	public float shrinkPercentage;
	public GameObject ChildBar;
	public float flashTimer = 0;
	public float shrinkTimer = 250;
	public string statee = "Growing";

	// Use this for initialization
	void Start () {
		maxSize = this.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(statee == "Growing"){

			growPercentage = 1 - ((GameLoop.bonusModeScoreThreshold - GameLoop.playerScore) / 1000);

			Vector3 temp = this.transform.localScale;
			temp.y = growPercentage * maxSize;
			ChildBar.GetComponent<Renderer>().material.color = new Color(1,1,1 - growPercentage);
			this.transform.localScale = temp;

			if(growPercentage > 0.85 && flashTimer < 25){
				ChildBar.GetComponent<Renderer>().enabled = false;
				flashTimer ++;
			}
			 else if(growPercentage > 0.85 && flashTimer >= 25){
				ChildBar.GetComponent<Renderer>().enabled = true;
				flashTimer ++;
			}
			 else if(growPercentage < 0.85){
				ChildBar.GetComponent<Renderer>().enabled = true;
			}

			if(growPercentage > 1){

			if(GameObject.Find("GameManager").GetComponent<GameLoop>().gameMode == "Normal"){
				ChildBar.GetComponent<Renderer>().enabled = true;
				statee = "Shrinking";
			} else{
					growPercentage = 1;
			}

			}

			if(flashTimer > 50){
				flashTimer = 0;
			}
		} else {

			shrinkPercentage = shrinkTimer / 250;
			shrinkTimer --;
			Vector3 temp = this.transform.localScale;
			temp.y = shrinkPercentage * maxSize;
			ChildBar.GetComponent<Renderer>().material.color = new Color(1,1,1 - shrinkPercentage);
			this.transform.localScale = temp;

			if(shrinkTimer <= 0){
				shrinkTimer = 250;
				statee = "Growing";
			}
		}

	}
}
