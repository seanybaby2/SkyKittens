using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ScriptsPlayerShipsFirstShip : MonoBehaviour {


	//all variables controlling movement go here
	public float curFWDSpeed = 0.0f;
	public float curROTSpeed = 0.0f;
	public float acc = 30.0f;
	public float rotAcc = 15.0f;
	public float maxSpeed = 60.0f;
	public float maxROTSpeed = 30.0f;

	//all variables controlling cannonFire go here
	public GameObject cannonBallPrefab;
	public GameObject bombPrefab;
	/*
	private Transform rightLauncher1;
	private Transform rightLauncher2;
	private Transform rightLauncher3;
	private Transform leftLauncher1;
	private Transform leftLauncher2;
	private Transform leftLauncher3;
	private Transform frontLauncher;
	private Transform bombLauncher1;
	private Transform bombLauncher2;
	*/
	//random cannonFire variables
	private double randomValue1;
	private double randomValue2;
	private double randomValue3;
	//zoom in boolean
	private bool shift = false;

	//transforms for cannonParticles
	/*
	private Transform leftCannonParticle3;
	private Transform leftCannonParticle2;
	private Transform leftCannonParticle1;
	private Transform rightCannonParticle3;
	private Transform rightCannonParticle2;
	private Transform rightCannonParticle1;
	private Transform frontCannonParticle1;
	*/

	public float cannonCooldown = 3;
	public float cannonSpeed = 150.0f;
	

	private float fireAudioTimer = 0;
	private float aimAudioTimer = 0;

	public static float maxHealth = 300.0f;
	public static float health = 300.0f;
	public static float damage = 25.0f;
	private static float tempHealth = 300.0f;

	private bool ready = true;
	private double reloadTimer;
	private int ammunition = 3;

	private Transform paw1;
	private Transform paw2;
	private Transform paw3;
	private Transform paw4;
	private Transform paw5;
	private Transform paw6;


	private Transform prop;
	private Transform rightCamera;
	private Transform leftCamera;
	private Transform frontCamera;
	private Transform rearCamera;
	private Transform shipBody;

	//for location enemy objects
	private Transform boardingDockPoint;
	public float boardingSpeed = 3.0f;
	public bool isBoarding = false;

	private GameObject rightSpotLight;
	private GameObject leftSpotLight;
	private GameObject frontSpotLight;

	public AudioClip cannonFireAudio;
	public AudioClip propPur;
	public AudioClip gotHit;
	public AudioClip gotHit2;
	public AudioClip angryCat;
	public AudioClip fire;
	public AudioClip aim;

	private double rightRandomFireTime1 = 5;
	private double rightRandomFireTime2 = 5;
	private double rightRandomFireTime3 = 5;

	private double leftRandomFireTime1 = 5;
	private double leftRandomFireTime2 = 5;
	private double leftRandomFireTime3 = 5;

	
	//these are our y rotational variables used to make rotating look more natural
	private float rotateForward;
	private float rotateHorizontal;

	public TextMesh upgradeGold;
	public TextMesh upgradeGoldl;
	public TextMesh upgradeGoldr;
	public TextMesh upgradeGoldf;
	public TextMesh upgradeGoldrear;

	//these variables are for controlling third person mode
	private bool thirdPersonMode;


	//this is the component for our ship.
	//learn scrum
	CharacterController ship;

	//start is called once at the beginning of the script
	private void Start(){
		ship = GetComponent<CharacterController>();
		shipBody = transform.Find ("Body");
		//set our ships stats
		/*
		acc = 45.0f;
		rotAcc = 30.0f;
		maxSpeed = 80.0f;
		maxROTSpeed = 50.0f;
		damage = 25.0f;
		cannonCooldown = 3.0f;
		health = 300.0f;
		maxHealth = 300.0f;
		tempHealth = 300.0f;


		//get our cameras
		rightCamera = transform.Find ("Body/Right Camera");
		leftCamera = transform.Find ("Body/Left Camera");
		frontCamera = transform.Find ("Body/Front Camera");
		rearCamera = transform.Find ("Body/Rear Camera");

		//get our spotlight
		rightSpotLight = GameObject.Find ("Right Spotlight");
		leftSpotLight = GameObject.Find ("Left Spotlight");
		frontSpotLight = GameObject.Find ("Front Spotlight");

		//turn off our spotlights
		rightSpotLight.light.enabled = false;
		leftSpotLight.light.enabled = false;
		frontSpotLight.light.enabled = false;

		//make sure we have our charater controller component and our shipBody
		ship = GetComponent<CharacterController>();
		shipBody = transform.Find ("Body");



		//make sure we have our cannon transforms
		rightLauncher1 = transform.Find("RightCannon1");
		rightLauncher2 = transform.Find("RightCannon2");
		rightLauncher3 = transform.Find("RightCannon3");
		leftLauncher1 = transform.Find("LeftCannon1");
		leftLauncher2 = transform.Find("LeftCannon2");
		leftLauncher3 = transform.Find("LeftCannon3");
		frontLauncher = transform.Find ("FrontCannon1");
		bombLauncher1 = transform.Find("Bomb Launcher1");
		bombLauncher2 = transform.Find("Bomb Launcher2");

		//get our particle effects transforms
		leftCannonParticle3 = transform.Find ("Body/Left CannonFire3");
		leftCannonParticle2 = transform.Find ("Body/Left CannonFire2");
		leftCannonParticle1 = transform.Find ("Body/Left CannonFire1");
		rightCannonParticle3 = transform.Find ("Body/Right CannonFire3");
		rightCannonParticle2 = transform.Find ("Body/Right CannonFire2");
		rightCannonParticle1 = transform.Find ("Body/Right CannonFire1");
		frontCannonParticle1 = transform.Find ("Body/Front CannonFire1");
	


		//make sure we have our paw cooldown transforms
		paw1 = transform.Find ("Main Camera/Shot1");
		paw2 = transform.Find ("Main Camera/Shot2");
		paw3 = transform.Find ("Main Camera/Shot3");
		//make sure we have our prop transform
		prop = transform.Find ("Body/Prop");


		//set all of our upgrades	
		//damage

		damage += UpgradeShop.damageUpgrades * 10; //0-25, 1-35, 2-45, 3-55
		//health
		maxHealth += UpgradeShop.hullUpgrades * 100; //0-300 1-400 2-500 3-600
		tempHealth += UpgradeShop.hullUpgrades * 100; //this makes sure we set our temp health as well as our health
		//speed
		acc += UpgradeShop.speedUpgrades * 10; //0-40 1-50 2-60 3-70
		rotAcc += UpgradeShop.speedUpgrades * 5; //0-25 1-30 2-35 3-40 
		maxSpeed += UpgradeShop.speedUpgrades * 20; //0-80 0-100 0-120 0-140
		maxROTSpeed += UpgradeShop.speedUpgrades * 5; //0-40 1-45 2-50 3-55
		cannonCooldown -= UpgradeShop.cooldownUpgrades * 0.5f; // 0-3.0 1-2.5 2-2.0 3-1.5
		tempHealth = maxHealth;
		health = maxHealth;
		updateHealth();
		*/


	}

	//OLD SCRIPT NOT NECESSARY NOW
	//update is called every frame
	private void Update(){

		if(thirdPersonMode){
			thirdPersonMovement();
		}

		if(!thirdPersonMode){
			shipMovement();
		}

		/*
		upgradeGold.text = UpgradeShop.goldPlundered.ToString();
		upgradeGoldf.text = UpgradeShop.goldPlundered.ToString();
		upgradeGoldr.text = UpgradeShop.goldPlundered.ToString();
		upgradeGoldrear.text = UpgradeShop.goldPlundered.ToString();
		upgradeGoldl.text = UpgradeShop.goldPlundered.ToString();

		//if(menuManager.gameStart == true){

			//movement controls our ships directional changes
			movement();
			//cannonFire controls our ships cannons
			cannonFire();

			if(health <=0){
				if(GameManager.currentLevel == 1){
					audio.PlayOneShot(angryCat, 1.0f);
					health = maxHealth;
					tempHealth = maxHealth;
					updateHealth ();
				Application.LoadLevel("level1");
				}
			if(GameManager.currentLevel == 2){
				audio.PlayOneShot(angryCat, 1.0f);
				health = maxHealth;
				tempHealth = maxHealth;
				updateHealth ();
				Application.LoadLevel("level2");

			}
			if(GameManager.currentLevel == 3){
				audio.PlayOneShot(angryCat, 1.0f);
				health = maxHealth;
				tempHealth = maxHealth;
				updateHealth ();
				BossTrigger.bossFight = false;
				Application.LoadLevel("level3");
			}
		}
		
		if(health < tempHealth){

				audio.PlayOneShot(gotHit, 0.7f);
				audio.PlayOneShot(gotHit2, 1.0f);
				tempHealth = health;
				updateHealth ();
			}
		//}

		if(Input.GetKey("p")){
			//menuManager.swapScreen(screenName.PAUSE);
		}

	
	}*/

	/*public void updateHealth(){
		Color pawHealth1 = new Color(1,0,0, health) * .01f;
		Color pawHealth2 = new Color(1,0,0, health - 100) * .01f;
		Color pawHealth3 = new Color(1,0,0, health - 200) * .01f;
		Color pawHealth4 = new Color(1,0,0, health - 300) * .01f;
		Color pawHealth5 = new Color(1,0,0, health - 400) * .01f;
		Color pawHealth6 = new Color(1,0,0, health - 500) * .01f;

	
		GameObject.Find("Paw1").transform.renderer.material.SetAlpha(GameObject.Find("Paw1").renderer.material.color.a, health * .01f);
		GameObject.Find("Paw2").transform.renderer.material.SetAlpha(GameObject.Find("Paw2").renderer.material.color.a, (health - 100) * .01f);
		GameObject.Find("Paw3").transform.renderer.material.SetAlpha(GameObject.Find("Paw3").renderer.material.color.a, (health - 200) * .01f);
		GameObject.Find("Paw4").transform.renderer.material.SetAlpha(GameObject.Find("Paw4").renderer.material.color.a, (health - 300) * .01f);
		GameObject.Find("Paw5").transform.renderer.material.SetAlpha(GameObject.Find("Paw5").renderer.material.color.a, (health - 400) * .01f);

		GameObject.Find("Paw1").transform.renderer.material.color = new Color(255,0,0, health) * .01f;
		GameObject.Find("Paw2").transform.renderer.material.color = new Color(255,0,0, health - 100) * .01f;
		GameObject.Find("Paw3").transform.renderer.material.color = new Color(255,0,0, health - 200) * .01f;
		GameObject.Find("Paw4").transform.renderer.material.color = new Color(255,0,0, health - 300) * .01f;
		GameObject.Find("Paw5").transform.renderer.material.color = new Color(255,0,0, health - 400) * .01f;
		GameObject.Find("Paw6").transform.renderer.material.color = new Color(255,0,0, health - 500) * .01f;  //(GameObject.Find("Paw6").renderer.material.color.a, (health - 500) * .01f);

		GameObject.Find("LeftPaw1").transform.renderer.material.color = new Color(255,0,0, health) * .01f;
		GameObject.Find("LeftPaw2").transform.renderer.material.color = new Color(255,0,0, health - 100) * .01f;
		GameObject.Find("LeftPaw3").transform.renderer.material.color = new Color(255,0,0, health - 200) * .01f;
		GameObject.Find("LeftPaw4").transform.renderer.material.color = new Color(255,0,0, health - 300) * .01f;
		GameObject.Find("LeftPaw5").transform.renderer.material.color = new Color(255,0,0, health - 400) * .01f;
		GameObject.Find("LeftPaw6").transform.renderer.material.color = new Color(255,0,0, health - 500) * .01f; 

		GameObject.Find("RightPaw1").transform.renderer.material.color = new Color(255,0,0, health) * .01f;
		GameObject.Find("RightPaw2").transform.renderer.material.color = new Color(255,0,0, health - 100) * .01f;
		GameObject.Find("RightPaw3").transform.renderer.material.color = new Color(255,0,0, health - 200) * .01f;
		GameObject.Find("RightPaw4").transform.renderer.material.color = new Color(255,0,0, health - 300) * .01f;
		GameObject.Find("RightPaw5").transform.renderer.material.color = new Color(255,0,0, health - 400) * .01f;
		GameObject.Find("RightPaw6").transform.renderer.material.color = new Color(255,0,0, health - 500) * .01f;

		GameObject.Find("FrontPaw1").transform.renderer.material.color = new Color(255,0,0, health) * .01f;
		GameObject.Find("FrontPaw2").transform.renderer.material.color = new Color(255,0,0, health - 100) * .01f;
		GameObject.Find("FrontPaw3").transform.renderer.material.color = new Color(255,0,0, health - 200) * .01f;
		GameObject.Find("FrontPaw4").transform.renderer.material.color = new Color(255,0,0, health - 300) * .01f;
		GameObject.Find("FrontPaw5").transform.renderer.material.color = new Color(255,0,0, health - 400) * .01f;
		GameObject.Find("FrontPaw6").transform.renderer.material.color = new Color(255,0,0, health - 500) * .01f;

		GameObject.Find("RearPaw1").transform.renderer.material.color = new Color(255,0,0, health) * .01f;
		GameObject.Find("RearPaw2").transform.renderer.material.color = new Color(255,0,0, health - 100) * .01f;
		GameObject.Find("RearPaw3").transform.renderer.material.color = new Color(255,0,0, health - 200) * .01f;
		GameObject.Find("RearPaw4").transform.renderer.material.color = new Color(255,0,0, health - 300) * .01f;
		GameObject.Find("RearPaw5").transform.renderer.material.color = new Color(255,0,0, health - 400) * .01f;
		GameObject.Find("RearPaw6").transform.renderer.material.color = new Color(255,0,0, health - 500) * .01f;
		*/
	}



	private void thirdPersonMovement(){
		//asd

	}

	//this method controls the ships movement, acceleration, decelleration as well as rotation
	public void shipMovement(){


		rotateForward = curFWDSpeed;
		rotateHorizontal = curROTSpeed;




		//this code keeps us from moving off of our set boundary position
		if(transform.position.y != 40)
			transform.position = new Vector3(this.transform.position.x, 40, this.transform.position.z);


		//prop.transform.Rotate( 0, 0 , Time.deltaTime * (7 * curFWDSpeed));

		//these two sets are meant to equalize speed if a player isnt pressing a button
		if(curFWDSpeed > 0 && Input.GetKey("w") == false)
			curFWDSpeed -= (acc/2) * Time.deltaTime;
		if(curFWDSpeed < 0 && Input.GetKey("s") == false)
			curFWDSpeed += (acc/2) * Time.deltaTime;


		//maxSpeed equation for greater than
		if(curFWDSpeed > maxSpeed)
			curFWDSpeed = maxSpeed;

		//maxSpeed equation for less than
		if((curFWDSpeed) < (maxSpeed * -1))
			curFWDSpeed = maxSpeed * -1;

		if(Input.GetKey("w")){
			curFWDSpeed += acc * Time.deltaTime;
			if(shipBody.eulerAngles.x > 356 || shipBody.eulerAngles.x < 200  && curFWDSpeed > 0)
				shipBody.Rotate(Time.deltaTime * -(rotateForward/8), 0, 0);
		}
		if(Input.GetKey("s")){
			curFWDSpeed -= acc * Time.deltaTime;
			if(shipBody.eulerAngles.x < 4 || shipBody.eulerAngles.x > 200 && curFWDSpeed < 0)
				shipBody.Rotate(Time.deltaTime * -(rotateForward/8), 0, 0);
		}

		Vector3 forward =  transform.TransformDirection(Vector3.forward) * curFWDSpeed;
		ship.Move(forward * Time.deltaTime);



		//rotational commands
		if(Input.GetKey ("d")){
			curROTSpeed += rotAcc * Time.deltaTime;
			if(shipBody.eulerAngles.z > 357 || shipBody.eulerAngles.z < 200  && curROTSpeed > 0)
				shipBody.Rotate(0, 0, Time.deltaTime * -(rotateHorizontal/8));
		}
		if(Input.GetKey("a")){
			curROTSpeed -= rotAcc * Time.deltaTime;
			if(shipBody.eulerAngles.z < 3 || shipBody.eulerAngles.z > 200  && curROTSpeed < 0)
				shipBody.Rotate(0, 0, Time.deltaTime * -(rotateHorizontal/8));
		}

		if(curROTSpeed > maxROTSpeed)
			curROTSpeed= maxROTSpeed;

		if(curROTSpeed < maxROTSpeed * -1)
			curROTSpeed = maxROTSpeed * -1;
		
		//maxSpeed equation for less than
		if((curFWDSpeed) < (maxSpeed * -1))
			curFWDSpeed = maxSpeed * -1;


		if(curROTSpeed > 0 && Input.GetKey("d") == false){
			curROTSpeed -= (rotAcc/2) * Time.deltaTime;
		}
		if(curROTSpeed < 0 && Input.GetKey("a") == false){
			curROTSpeed += (rotAcc/2) * Time.deltaTime;
		}

		//these two statements reset x and z rotational values
		if(shipBody.eulerAngles.z > 0 && shipBody.eulerAngles.z > 200 && Input.GetKey("d") == false && Input.GetKey ("a") == false){
			shipBody.Rotate(0, 0, Time.deltaTime * (rotateHorizontal/16 + 0.5f));
		}
		if(shipBody.eulerAngles.z > 0 && shipBody.eulerAngles.z < 200 && Input.GetKey("d") == false && Input.GetKey ("a") == false){
			shipBody.Rotate(0, 0, Time.deltaTime * (rotateHorizontal/16 - 0.5f));
		}


		if(shipBody.eulerAngles.x > 0 && shipBody.eulerAngles.x > 200 && Input.GetKey("w") == false && Input.GetKey ("s") == false){
			shipBody.Rotate(Time.deltaTime * (rotateForward/16 + 0.5f), 0, 0);
		}
		if(shipBody.eulerAngles.x > 0 && shipBody.eulerAngles.x < 200 && Input.GetKey("w") == false && Input.GetKey ("s") == false){
			shipBody.Rotate(Time.deltaTime * (rotateForward/16 - 0.5f), 0, 0);
		}

		transform.Rotate (new Vector3(0, (curROTSpeed) * Time.deltaTime, 0));


		}//end of method movement()
	

	//temporary health GUI
	void OnGUI () {
		//GUI.Box(new Rect(70,10,100,30), "HEALTH " + health);
		//GUI.Box(new Rect(200,10,150,30), "Gold Plundered " + UpgradeShop.goldPlundered);
	}

	//terrain collission for the player
	void OnControllerColliderHit(ControllerColliderHit col){
		/*if(col.gameObject.tag == "terrain"){
			if(curFWDSpeed > 0)
				curFWDSpeed = (curFWDSpeed * -1) - 10;
			else if(curFWDSpeed < 0)
				curFWDSpeed = (curFWDSpeed * -1) + 10;
			ShipController.health -= 3;

			}*/
	}

	public IEnumerator boardLocation(){
		float step = boardingSpeed * Time.deltaTime;
		Vector3.MoveTowards(this.transform.position, this.boardingDockPoint.position, step);


		yield return new WaitForSeconds(3f);
		ship.enabled = false;
	}
			
}//end of class ShipController


