using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class ScriptsPlayerCharacterThirdPerson : MonoBehaviour {


	//all variables concerning the player
	//these are for the players look direction
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;

	//these are for the players running
	float rotationY = 0F;
	public float runSpeed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public Texture2D crosshairTexture;
	private Rect crosshairPosition;
	private Transform thirdPersonFireLocation;
	public Rigidbody bulletPrefab;

CharacterController catController;

	void Start(){
		catController = GetComponent<CharacterController>();
		crosshairPosition = new Rect((Screen.width - crosshairTexture.width) / 2 - 20, (Screen.height - 
		                                                              crosshairTexture.height) /2 - 5, crosshairTexture.width, crosshairTexture.height);
		thirdPersonFireLocation = transform.Find("Main Camera/ThirdPersonFireLocation");
	
		Screen.showCursor = false;

	}
	
		
	void Update (){
		if(ScriptsPlayerShipsFirstShip.thirdPersonMode){
			catController.enabled = true;

			if (axes == RotationAxes.MouseXAndY){
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}


		
			if (catController.isGrounded) {
				Debug.Log ("hi");
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= runSpeed;
				if (Input.GetButton("Jump"))
					 moveDirection.y = jumpSpeed;
			}
				moveDirection.y -= gravity * Time.deltaTime;
				catController.Move(moveDirection * Time.deltaTime);

			if(Input.GetMouseButtonDown(0)){
				Rigidbody bulletTemp = Instantiate(bulletPrefab, thirdPersonFireLocation.position, thirdPersonFireLocation.rotation) as Rigidbody;
				bulletTemp.rigidbody.AddForce(transform.forward * 5000);
			}
		}
		else{
			catController.enabled = false;
		}
			
		//end of third person script
	}


	void OnGUI()
	{
			if(ScriptsPlayerShipsFirstShip.thirdPersonMode)
				GUI.DrawTexture(crosshairPosition, crosshairTexture);
	}
}



/*
public float speed = 6.0F;
public float jumpSpeed = 8.0F;
public float gravity = 20.0F;
private Vector3 moveDirection = Vector3.zero;
void Update() {
	CharacterController controller = GetComponent<CharacterController>();
	if (controller.isGrounded) {
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		if (Input.GetButton("Jump"))
			moveDirection.y = jumpSpeed;
		
	}
	moveDirection.y -= gravity * Time.deltaTime;
	controller.Move(moveDirection * Time.deltaTime);
}
}

*/
