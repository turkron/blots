using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour {

	public int Timer = 250;
	private string ThisText;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Timer --;

		if(Timer < 200 && Timer > 150){
			this.GetComponent<TextMesh>().text = "3";
		}
		if(Timer < 150 && Timer > 100){
			this.GetComponent<TextMesh>().text = "2";
		}
		if(Timer < 100 && Timer > 50){
			this.GetComponent<TextMesh>().text = "1";
		}
		if(Timer < 50 && Timer > 0){
			this.GetComponent<TextMesh>().text = "Go!";
		}

		if(Timer <= 0){
			GameObject.Destroy(this.gameObject);
		}
	
	}
}
