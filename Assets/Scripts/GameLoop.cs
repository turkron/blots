using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {

	public string OverallGameMode = "n/a";
	public string gameMode = "n/a";
	public int Timer = 150;
	public GameObject modeTextObject;
	public GameObject infoTextObj;

	public int normalModeDuration = 500;
	public int bonusModeDuration = 500;
	public int bombModeDuration = 500;
	public int chainModeDuration = 500;
	public int massModeDuration = 500;
	public int greyModeDuration = 500;


	public GameObject normalDot;
	public static int normalDotCount = 0;
	public int maxNormalDots = 25;

	public GameObject bonusDot;
	public static int bonusDotCount = 0;
	public int maxBonusDots = 10;

	public GameObject bombTargetDot;
	public static int bombTargetDotCount = 0;
	public int maxBombTargetDots = 10;
	public GameObject bombDot;
	public static int bombDotCount = 0;
	public int maxBombDots = 10;

	public GameObject chainDot;
	public static int chainDotCount = 0;
	public int maxChainDots = 20;
	public static int frozenDotCount = 0;
	public float chainGoal;
	public static int freezeTimer = 0;

	public GameObject[] patternArray;
	static public float massDotCount = 0;
	static public float maxMassDots = 0;


	public GameObject greyDot;
	public int maxGreyDots = 10;
	static public int greyDotCount;
	public GameObject blueDot;
	public int maxBlueDots = 10;
	static public int blueDotCount;

	public static float playerTimer = 3250;
	public static float playerScore = 0;

	public static float bonusModeScoreThreshold = 1000;




	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void FixedUpdate () {

	

		if(playerTimer <= 0){
			print("time to end the game");
			//record score and go to end screen;
		} else {

			playerTimer --;
			Timer --;

			if(OverallGameMode == "Classic") {
			
				if(gameMode == "Intro"){

					if(Timer <= 0){
						gameMode = "Normal";
						Timer = normalModeDuration;
					}
				}
			
				if(gameMode == "Normal"){
					if(normalDotCount < maxNormalDots){
						InstantiateDot(normalDot, 10, 300);
					}
					if(playerScore > bonusModeScoreThreshold && gameMode == "Normal"){
						gameMode = "Bonus";
						Timer = bonusModeDuration;
						GameObject modeText1 = (GameObject)Instantiate(modeTextObject,new Vector3(0,0,0), Quaternion.Euler(0,0,0));
					}


					if(Timer == 0){
						int randomGameMode = Random.Range(1,5);

						GameObject modeText1 = (GameObject)Instantiate(modeTextObject,new Vector3(0,0,0), Quaternion.Euler(0,0,0));

						if(randomGameMode == 1){
							gameMode = "Bomb";
							Instantiate(infoTextObj, new Vector2(0,-1.5f),Quaternion.Euler(0,0,0));
							Timer = bombModeDuration;
						} else if(randomGameMode == 2){
							gameMode = "Chain";
							Instantiate(infoTextObj, new Vector2(0,-1.5f),Quaternion.Euler(0,0,0));
							chainGoal = Random.Range(5,15);
							Timer = chainModeDuration;
						} else if(randomGameMode == 3){
							gameMode = "Mass";
							Instantiate(infoTextObj, new Vector2(0,-1.5f),Quaternion.Euler(0,0,0));
							int randomMassPattern = Random.Range(0,patternArray.Length + 1);
							print("Array length: " +patternArray.Length);
							print("Assigned random: " + randomMassPattern);
							//NOTES:
							//need more patterns, 

							Instantiate(patternArray[randomMassPattern], new Vector2(0,0), Quaternion.Euler(0,0,0));

							Timer = massModeDuration;
						} else {
							gameMode = "Grey";
							Instantiate(infoTextObj, new Vector2(0,-1.5f),Quaternion.Euler(0,0,0));
							Timer = greyModeDuration;
						}

					}
				}

				if(gameMode == "Bonus"){
					//enter bonus mode
					if(Timer > 1){
						if(bonusDotCount < maxBonusDots){
							InstantiateDot(bonusDot, 0, 50);
						}

					} else {
						// exit and return to normal.
						Timer = normalModeDuration;
						gameMode = "Normal";
						bonusModeScoreThreshold = playerScore + 1000;
					}
				}

				if(gameMode == "Bomb"){
					if(Timer > 1){
						if(bombDotCount < maxBombDots){
							InstantiateDot(bombDot, 50, 100);
						}
						if(bombTargetDotCount < maxBombTargetDots){
							InstantiateDot(bombTargetDot ,10, 50);
						}
					} else {
						Timer = normalModeDuration;
						gameMode = "Normal";
					}

				}

				if(gameMode == "Chain"){
					//print(freezeTimer);
					if(frozenDotCount < chainGoal){
						if(freezeTimer >= 1){
							freezeTimer --;
						}
						if(freezeTimer == 0){
							frozenDotCount = 0;
						}


						if(Timer > 1){
							if(chainDotCount < maxChainDots){
								InstantiateDot(chainDot, 10, 50);
							}
						} else {
							Timer = normalModeDuration;
							frozenDotCount = 0;
							infoText.howWellDidYouDo = "Failed";
							gameMode = "Normal";
						}

					} else {
						//insert if statements here about mesuring the players performace vs time.
						infoText.howWellDidYouDo = "Great";

						Timer = normalModeDuration;
						freezeTimer = 0;
						gameMode = "Normal";
					}
					
				}

				if(gameMode == "Mass"){
					if(maxMassDots != 0){
						if(massDotCount < maxMassDots){

							if(Timer < 1){
								// rate performance here.
								if(massDotCount == 0){
									infoText.howWellDidYouDo = "Failed";
								}

								if(massDotCount <= (maxMassDots/3) && massDotCount != 0){
									infoText.howWellDidYouDo = "Poor";
								}
								if(massDotCount <= ((maxMassDots/3) * 2) && massDotCount > (maxMassDots/3) ){
									infoText.howWellDidYouDo = "Okay";
								}

								Timer = normalModeDuration;
								massDotCount = 0;
								maxMassDots = 0;
								gameMode = "Normal";
							}

						} else {
							infoText.howWellDidYouDo = "Great";
							massDotCount = 0;
							maxMassDots = 0;
							Timer = normalModeDuration;
							gameMode = "Normal";
						}
					}
					
				}

				if(gameMode == "Grey"){
					if(Timer > 1){
						if(greyDotCount < maxGreyDots){
							InstantiateDot(greyDot,50,150);
						}
						if(blueDotCount < maxBlueDots){
							InstantiateDot(blueDot,50,150);
						}


						
					} else {
						Timer = normalModeDuration;
						gameMode = "Normal";
					}
					
				}


			}
		}
	}

	void InstantiateDot(GameObject InputGameObject, int lowerRange , int upperRange){
		Vector2 position = new Vector2(Random.Range(-6.127153f,6.093296f), Random.Range(4.465304f,-4.480673f));
		GameObject tempdot1 = (GameObject)Instantiate(InputGameObject, position, Quaternion.Euler(90,0,0));
		if(InputGameObject.tag == "bonusDot")
			bonusDotCount++;
		if(InputGameObject.tag == "normalDot")
			normalDotCount++;
		if(InputGameObject.tag == "bombTargetDot")
			bombTargetDotCount++;
		if(InputGameObject.tag == "bombDot")
			bombDotCount++;
		if(InputGameObject.tag == "chainDot")
			chainDotCount++;
		if(InputGameObject.tag == "blueDot")
			blueDotCount++;
		if(InputGameObject.tag == "greyDot")
			greyDotCount++;
		tempdot1.GetComponent<growAndShrink>().Timer = Random.Range(lowerRange, upperRange);
	}

}
