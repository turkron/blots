using UnityEngine;
using System.Collections;

public class growAndShrink : MonoBehaviour {

public string statee = "Delay";
public int Timer = 0;
public int dotLifeSpan = 200;
public float growSpeed = 0.05f;
public float shrinkSpeed = 0.01f;
public GameObject DText;
public GameObject GameManager;
public float maxScaleSize;
public int scoreWorth;
public string myMode;
public int freezeTimer = 0;
private Color originalColor;

	// Use this for initialization
	void Start () {
		GameManager = GameObject.Find("GameManager");
		originalColor = this.GetComponent<Renderer>().material.color;
		if(this.gameObject.tag == "massDot")
			GameLoop.maxMassDots ++;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(GameManager.GetComponent<GameLoop>().gameMode != myMode){
			DestroyMe(true,"",0);
		}

		if(statee == "Delay"){
			if(Timer > 0)
			Timer--;
			else
			statee = "Grow";
		}

		if (statee == "Grow"){
			this.transform.localScale += new Vector3(growSpeed, 0, growSpeed);
		}
		if(this.transform.localScale.x >= maxScaleSize && statee == "Grow"){
			statee = "Fully Grown"; // change this to "Fully Grown" to keep at full size;
			Timer ++;
		}

		if(statee == "Fully Grown"){
			Timer ++;
		}

		if(Timer >= dotLifeSpan && statee == "Fully Grown"){
			statee = "Shrink";
			this.transform.localScale = new Vector3(maxScaleSize - shrinkSpeed ,0.1f,maxScaleSize - shrinkSpeed);
		}

		if(statee == "Shrink"){
			this.transform.localScale -= new Vector3(shrinkSpeed, 0, shrinkSpeed);
		}

		if(this.transform.localScale.x <= 0){
			DestroyMe(true,"",0);
		}

		/// reserved for BombDot.
		if(statee == "Bomb"){
			Timer ++;
			this.transform.localScale = Vector3.Lerp(this.transform.localScale,new Vector3(100f,0.1f,100f),Time.deltaTime * 0.5f);
			if(Timer == 10){
				statee = "KillAllDots";
				Timer = 0;
			}
		}

		if(statee == "KillAllDots"){
			Timer ++;
			if(Timer == 3){
				DestroyMe(false,"",0);
			}
		}
		/// End of resevred space.

		// reserved for chainDot Class
		if(statee == "Frozen"){

			if(GameLoop.freezeTimer == 0){
				this.GetComponent<Renderer>().material.color = originalColor;
				statee = "Shrink";
			}
			this.GetComponent<Renderer>().material.color = new Color(0,0,255,100);

		};


		/// End of resevred space.
	}




	private Vector3 screenPoint;


	void OnMouseDown(){
		if(this.gameObject.tag == "bombDot"){
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		}
	}

	void OnMouseDrag(){
		if(this.gameObject.tag == "bombDot"){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
		transform.position = curPosition;
		}
	}

	void OnMouseUp(){

		// reserved for BombDot Class

		if(this.gameObject.tag == "bombDot"){ 
			Timer = 0;
			maxScaleSize = maxScaleSize + 10; // what is this line?
			statee = "Bomb";

		}// end of reserved space.


		// reserved for chainDot Class
		else if(this.gameObject.tag == "chainDot"){
				
				if(statee != "Frozen"){

					Timer = 0;
					GameLoop.freezeTimer = freezeTimer;
					GameLoop.frozenDotCount ++;
					this.transform.localScale = new Vector3(maxScaleSize,0.1f, maxScaleSize);
					this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
					statee = "Frozen";
					//print(statee);
				}
		}// end of reserved space.

		else if(this.gameObject.tag == "bombTargetDot"){
			DestroyMe(false,"Use the bomb!",0);
		}

		else {
			if(this.gameObject.tag == "massDot"){
				GameLoop.massDotCount ++;
			}
			DestroyMe(false,"+ " + scoreWorth,scoreWorth);
		}

	}

	void OnTriggerStay2D(Collider2D hitMe){

		if(this.gameObject.tag == "bombTargetDot"){
			if(hitMe.gameObject.tag == "bombDot"){
				if(hitMe.transform.GetComponent<growAndShrink>().statee == "KillAllDots" || hitMe.transform.GetComponent<growAndShrink>().statee == "Bomb"){
				DestroyMe(false, "+ " + scoreWorth, scoreWorth);
				}
			}
		}
	}

	void DestroyMe(bool timedOut, string dtext, int dscore){
		if(this.gameObject.tag == "normalDot")
		GameLoop.normalDotCount --;

		if(this.gameObject.tag == "bonusDot")
		GameLoop.bonusDotCount --;

		if(this.gameObject.tag == "bombDot")
		GameLoop.bombDotCount --;

		if(this.gameObject.tag == "bombTargetDot")
		GameLoop.bombTargetDotCount --;

		if(this.gameObject.tag == "chainDot")
			GameLoop.chainDotCount --;

		if(this.gameObject.tag == "blueDot")
			GameLoop.blueDotCount --;

		if(this.gameObject.tag == "greyDot")
			GameLoop.greyDotCount --;

		if(timedOut == false){
		GameObject DeathText;
		DeathText = Instantiate(DText, this.transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		DeathText.GetComponent<TextMesh>().text = dtext;
		DeathText.transform.parent = null;
		GameLoop.playerTimer += dscore ;
		GameLoop.playerScore += dscore ;
		}
		GameObject.Destroy(this.gameObject);
	}
}
