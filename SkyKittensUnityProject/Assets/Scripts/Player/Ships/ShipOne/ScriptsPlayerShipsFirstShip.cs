using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ScriptsPlayerShipsFirstShip : MonoBehaviour {


	//all variables controlling movement go here
	public float curFWDSpeed = 0.0f;
	public float curROTSpeed = 0.0f;
	public float acc = 30.0f;
	public float rotAcc = 15.0f;
	public float minSpeed = 15.0f;
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
	private float timeBoarding;
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
	public static bool thirdPersonMode;
	private Transform thirdPersonCharacter;






	//this is the component for our ship.
	//learn scrum
	CharacterController ship;

	//start is called once at the beginning of the script
	private void Start(){
		ship = GetComponent<CharacterController>();
		shipBody = transform.Find ("Body");
		rearCamera = transform.Find ("ShipCamera");

		thirdPersonCharacter = transform.Find ("Body/ThirdPersonTest");
		//here i dissable the ships charactercontroller collider because it fing sucks and I use box colliders instead to detect collisions triggers etc...
		//however the character controller is NEEDED for the movement, which is why we use it in the first place
		ship.detectCollisions = false;
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
			rearCamera.camera.enabled = false;
			ship.enabled = false;
			thirdPersonCharacter.parent = null;

		}
	
		if(!isBoarding){
			shipMovement();
		}

		if(isBoarding){
			boarding();
		}

		if(Input.GetKeyDown("t")){
			thirdPersonMode = true;
		}
	}

	




	//this method controls the ships movement, acceleration, decelleration as well as rotation
	public void shipMovement(){


	



	
		//this code keeps us from moving off of our set boundary position
		if(transform.position.y != 40)
			transform.position = new Vector3(this.transform.position.x, 40, this.transform.position.z);

		if(!thirdPersonMode){
			rotateForward = curFWDSpeed;
			rotateHorizontal = curROTSpeed;

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
			if(curFWDSpeed < minSpeed)
				curFWDSpeed = minSpeed;

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
				shipBody.Rotate(0, 0, Time.deltaTime * (rotateHorizontal/8 + 0.5f));
			}
			if(shipBody.eulerAngles.z > 0 && shipBody.eulerAngles.z < 200 && Input.GetKey("d") == false && Input.GetKey ("a") == false){
				shipBody.Rotate(0, 0, Time.deltaTime * (rotateHorizontal/8 - 0.5f));
			}


			if(shipBody.eulerAngles.x > 0 && shipBody.eulerAngles.x > 200 && Input.GetKey("w") == false && Input.GetKey ("s") == false){
				shipBody.Rotate(Time.deltaTime * (rotateForward/8 + 0.5f), 0, 0);
			}
			if(shipBody.eulerAngles.x > 0 && shipBody.eulerAngles.x < 200 && Input.GetKey("w") == false && Input.GetKey ("s") == false){
				shipBody.Rotate(Time.deltaTime * (rotateForward/8 - 0.5f), 0, 0);
			}

			transform.Rotate (new Vector3(0, (curROTSpeed) * Time.deltaTime, 0));

			}
		}//end of method movement()

	//old terrain collission for the player
	void OnControllerColliderHit(ControllerColliderHit col){
		/*if(col.gameObject.tag == "terrain"){
			if(curFWDSpeed > 0)
				curFWDSpeed = (curFWDSpeed * -1) - 10;
			else if(curFWDSpeed < 0)
				curFWDSpeed = (curFWDSpeed * -1) + 10;
			ShipController.health -= 3;

			}*/
	}

	//here are  all of my OnTriggerEnter instances for the player
	void OnTriggerEnter(Collider other){
		//	cube.transform.rotation = capsule.transform.rotation;

		//here I test to see if the player is hitting a boarding location, if so I find the boarding point and set boarding to true for the player
		if(other.tag == "BoardingLocation"){
			timeBoarding = 0;
			isBoarding = true;
			boardingDockPoint = other.transform.Find("DockingPoint");
		}
	}

	//here is the actual boarding function
	void boarding(){
		//this.transform.Rotate(new Vector3( 0, boardingDockPoint.rotation.y, 0));
			
			float rotateDist = boardingDockPoint.eulerAngles.y - this.transform.eulerAngles.y;
			Debug.Log (rotateDist);
			timeBoarding += Time.deltaTime;

			if(timeBoarding < 7f){
				float moveStep = boardingSpeed * Time.deltaTime;
				float rotateStep = boardingSpeed * Time.deltaTime * 2f;
				transform.position = Vector3.MoveTowards(transform.position, boardingDockPoint.position, moveStep);
				if(rotateDist >0)
					transform.rotation = Quaternion.RotateTowards(transform.rotation, boardingDockPoint.rotation, rotateStep);
				if(rotateDist < 0)
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Inverse(boardingDockPoint.rotation), rotateStep);
		}
	}


	/*
	Vector3 targetDir = target.position - transform.position;
	float step = speed * Time.deltaTime;
	Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
	Debug.DrawRay(transform.position, newDir, Color.red);
	transform.rotation = Quaternion.LookRotation(newDir);
	*/
	
}//end of class ShipController


