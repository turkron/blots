using UnityEngine;
using System.Collections;

public class TimerFeedBack : MonoBehaviour {

	public float startSize;

	// Use this for initialization
	void Start () {

		startSize = this.transform.localScale.y;
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 temp = this.transform.localScale;
		temp.y = (GameLoop.playerTimer / 3000) * startSize;
		//print (GameLoop.playerTimer / 3000 * startSize);
		this.transform.localScale = temp;

		//place flashing code here if needed for polish.
	
	}
}
