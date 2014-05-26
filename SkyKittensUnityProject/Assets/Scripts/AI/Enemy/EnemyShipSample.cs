using UnityEngine;
using System.Collections;

public class EnemyShipSample : MonoBehaviour {

	
	//all variables controlling movement go here
	public float speed = 20;

	public Transform waypoint1;
	public Transform waypoint2;
	public Transform waypoint3;
	public Transform waypoint4;
	public Transform player;
	public GameObject explosion;
	public Rigidbody cannonBallPrefab;
	public float reloadTimer = 2;
	private Vector3 dir;
	private Transform cannon1;
	private Transform cannon2;
	public float cannonSpeed = 20.0f;
	public float damping = 4.0f;
	public float health = 60.0f;
	private float maxHealth;
	private Transform prop1;
	private Transform prop2;
	private Transform prop3;
	private Transform explosionPoint;
	private GameObject smoke;
	private float goldAmount;
	private bool dying = false;
	private bool hasStart = false;
	private bool hasStart2 = false;
	private bool almostDead = false;
	public AudioClip gotHit;

	private float randomFlip;




	// Use this for initialization
	void Start () {
		maxHealth = health;
		randomFlip = Random.Range(1,10);
		goldAmount = Random.Range(1500,2500);
		dir = waypoint1.position - transform.position;
		cannon1 = transform.Find("EnemyBlimp1/SideCannon1");
		cannon2 = transform.Find("EnemyBlimp1/SideCannon2");
		prop1 = transform.Find ("EnemyBlimp1/prop1");
		prop2 = transform.Find ("EnemyBlimp1/prop2");
		prop3 = transform.Find ("EnemyBlimp1/prop3");
		explosionPoint = transform.Find ("ExplosionPoint");
		smoke = transform.Find("SmokePoint1").gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if(health <= maxHealth/2 && hasStart2 ==  false){
			smoke.SetActive(true);
			hasStart2 = true;
		}

		prop1.transform.Rotate( 0, 0 , 200);
		prop2.transform.Rotate( 0, 0 , 200);
		prop3.transform.Rotate( 0, 0 , 200);

		if(dying == false){
			Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * (damping / 8));
		}

		if(dying == false)
			transform.Translate(dir * (speed/50) * Time.deltaTime, Space.World);

		enemyFire();
		if(health <= 0){
			transform.Rotate( randomFlip/4, 5 , randomFlip/2);
			transform.Translate(-Vector3.up * 25 * Time.deltaTime, Space.World);
			//transform.Translate(Vector3(0, -20, 0) * 20 * Time.deltaTime, Space.World);
			dying = true;
		}

		if(dying && hasStart == false){
			StartCoroutine("beginDeath");
			hasStart = true;
		}

		//this code keeps us from moving off of our set boundary position
		if(transform.position.y != 39 && dying == false)
			transform.position = new Vector3(this.transform.position.x, 39, this.transform.position.z);

	}

	IEnumerator beginDeath(){
	
		if(almostDead == false){
			yield return new WaitForSeconds(2);
				Instantiate(explosion, explosionPoint.position, transform.rotation);

				almostDead = true;
		}
		if(almostDead == true){
		yield return new WaitForSeconds(.1f);
//			UpgradeShop.goldPlundered += goldAmount;
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "waypoint1"){
			dir = waypoint2.position - transform.position;
		}
		if(col.tag == "waypoint2"){
			dir = waypoint3.position - transform.position;
		}
		if(col.tag == "waypoint3"){
			dir = waypoint4.position - transform.position;
		}
		if(col.tag == "waypoint4"){
			dir = waypoint1.position - transform.position;
		}
	}
	//demitriusjp@gmail.com
	//demetriuspennebaker.com
	//cubic advance learning systems
	//character and competence gets you a job in the game industry. period.

	//be good at working with other ppl, every1 wants to be a designer
	//the collective mind is greater than the singular mind
	//show your best 3 to 5 works only
	 

	void enemyFire(){
		RaycastHit hit;

		if(dying == false){
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(transform.position, fwd, out hit)){
				float distanceToTarget = hit.distance;
				if(hit.transform.tag == "Player" && reloadTimer <= 0 && distanceToTarget < 600){
					reloadTimer = 2;
					Rigidbody Ball0 = Instantiate(cannonBallPrefab, cannon1.position, transform.rotation) as Rigidbody;
					Rigidbody Ball1 = Instantiate(cannonBallPrefab, cannon2.position, transform.rotation) as Rigidbody;
					Ball0.rigidbody.AddForce(cannon1.transform.right * (cannonSpeed * 100));
					Ball1.rigidbody.AddForce(cannon2.transform.right * (cannonSpeed * 100));
				}
			}
			Debug.DrawLine(transform.position, fwd);
		
			if(reloadTimer > 0)
				reloadTimer -= Time.deltaTime;
		
		}

		}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == "Sphere(Clone)"){
			audio.PlayOneShot(gotHit, 0.7f);
			//health -= ShipController.damage;
		}
		if(col.gameObject.name =="Bomb(Clone)"){
			audio.PlayOneShot(gotHit, 0.7f);
			//health -= ShipController.damage * 2;
		}
	}
	

}
