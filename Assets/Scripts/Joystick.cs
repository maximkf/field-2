using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {

	public bool isButton, leftJoystick;
	public string buttonName;

	private Vector3 startPos;
	private Transform thisTransform;
	private MeshRenderer mr;

	// Use this for initialization
	private void Start () 
	{

		thisTransform = transform;
		startPos = thisTransform.position;
		mr = thisTransform.GetComponent<MeshRenderer> ();
	}

	// Update is called once per frame
	private void Update () 
	{
		if (isButton) 
		{
			mr.enabled = Input.GetButton (buttonName);
		} 
		else 
		{
			if(leftJoystick)
			{
				Vector3 inputDirection = Vector3.zero;
				inputDirection.x = Input.GetAxis("LeftJoystickHorizontal");
				inputDirection.z = Input.GetAxis("LeftJoystickVertical");
				thisTransform.position = startPos + inputDirection;
			}
			else
			{
				Vector3 inputDirection = Vector3.zero;
				inputDirection.x = Input.GetAxis("RightJoystickHorizontal");
				inputDirection.z = Input.GetAxis("RightJoystickVertical");
				thisTransform.position = startPos + inputDirection;
			}
		}
	}
}
