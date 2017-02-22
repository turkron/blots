using UnityEngine;
using System.Collections;

public class scoreText : MonoBehaviour {
	private int lifespan = 30;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifespan --;
		this.transform.position += new Vector3(0f,0.001f,0f);
		if(lifespan < 0)
			GameObject.Destroy(this.gameObject);
	}
}
