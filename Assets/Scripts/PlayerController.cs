using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	//RequireComponenet automatically adds the component indicated

	// Handling
	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;

	//System
	private Quaternion targetRotation;

	//Components
	private CharacterController controller;
	private Camera cam;

	void Start () {
		controller = GetComponent<CharacterController> ();
		cam = Camera.main; //need the cam reference for mouse position in world space
	}
	
	// Update is called once per frame
	void Update () {

		//could be a public variable switch statement to work in each mode as needed
		ControlMouse (); //More typical way of controlling tds
//		ControlWASD ();
//		ControlWASD (); 
	}

	void ControlGamePad(){
		//This is what we want eventually

		//form an array looking in the direction of the left-joystick angle

		//remove motion input and switch shooting for jumping
	}

	void ControlMouse(){
		//get the mouse position
		Vector3 mousePos = Input.mousePosition;

		//get the mouse position in world space using the mouse position and zeroing out the camera distance from player
		mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));

		//make a new vector3 for the current input values
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")); 

		// if input
		if (input != Vector3.zero) { 
			//form an array looking in the direction of the mouse
			targetRotation = Quaternion.LookRotation (mousePos - new Vector3(transform.position.x, 0, transform.position.z));

			//eulerAngles is a Vector3 way of handling object rotation in 3D space(?) Vector3.up is (0,1,0) (y-axis?) and MoveTowardsAngle is a kind of lerp
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		}

		//motion vector for moving in the direction of input
		Vector3 motion = input;

		//abreviated if statement placed in the right hand of an equation (condition) do(?) a else(:) b
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1; //if moving at an angle decrease speed because pythagoras
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;	//multiply by runspeed or walkspeed
		//		motion += Vector3.up * -8; //add the motion vector to the y-axis * -8 wtf? i have no idea why this works or if it's even needed

		//move the character controller
		controller.Move (motion * Time.deltaTime);

	}

	void ControlWASD(){
		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")); //makes a new vector3 for the current input values

		if (input != Vector3.zero) { //if input exists ...
			//targetRotation form an array pointing in the direction of the input vector
			targetRotation = Quaternion.LookRotation (input);

			//eulerAngles is a Vector3 way of handling object rotation in 3D space(?) Vector3.up is (0,1,0) (y-axis?) and MoveTowardsAngle is a kind of lerp
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		}

		//motion vector for moving in the direction of input
		Vector3 motion = input;

		//abreviated if statement placed in the right hand of an equation (condition) do(?) a else(:) b
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1; //if moving at an angle decrease speed because pythagoras
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;	//multiply by runspeed or walkspeed
//		motion += Vector3.up * -8; //add the motion vector to the y-axis * -8 wtf? i have no idea why this works or if it's even needed

		//move the character controller
		controller.Move (motion * Time.deltaTime);
	}
}
