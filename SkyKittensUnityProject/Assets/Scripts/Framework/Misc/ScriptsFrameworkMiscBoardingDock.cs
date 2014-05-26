using UnityEngine;
using System.Collections;

public class ScriptsFrameworkMiscBoardingDock : MonoBehaviour {

	//here are all of my acces variables
	private GameObject playerGameObject;
	public Transform boardingPoint;
	private ScriptsPlayerShipsFirstShip playerScript; 

	void Awake(){
		//Here I acces the player and the players game object
		playerGameObject = GameObject.FindGameObjectWithTag("Player");
		playerScript = playerGameObject.GetComponent<ScriptsPlayerShipsFirstShip>();
		boardingPoint = transform.Find("BoardingDock/DockPoint");
		Debug.Log (playerGameObject);
	}

	public void OnTriggerEnter(Collider other){

		//here I access the players script and start the boarding coroutine
		playerScript.StartCoroutine(playerScript.boardLocation());
	}
}
