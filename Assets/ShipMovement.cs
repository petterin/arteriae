using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	// The ship gives the movement it's direction
	public GameObject Ship;
	// This camera is translated relative to the ship
	public GameObject Camera;
	// Determines how much the velocity changes with respect to time passed
	public float Power = 1;
	// Determines how much the rotation changes with respect to time passed
	public float RotationPower = 1;
	// Determines how much the head must be turned to turn the ship
	public float RotationThresholdAngle = 25;
	
	public KeyCode MoveForwardKey = KeyCode.W;
	public KeyCode MoveBackwardKey = KeyCode.S;
	public KeyCode MoveLeftKey = KeyCode.A;
	public KeyCode MoveRightKey = KeyCode.D;
	public KeyCode MoveUpKey = KeyCode.O;
	public KeyCode MoveDownKey = KeyCode.L;
	
	// Use this for initialization
	void Start () {
		if(!this.Ship) {
			throw new UnityException("No ship detected in ShipMovement!");
		}
		if(!this.Camera) {
			throw new UnityException("No camera detected in ShipMovement!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		float acceleration = Time.deltaTime * this.Power;
		Vector3 direction = new Vector3(0,0,0);
		
		// Handling translation first 
		if(Input.GetKey(this.MoveForwardKey)) {
			direction += this.Ship.transform.forward;
		}
		if(Input.GetKey(this.MoveBackwardKey)) {
			direction -= this.Ship.transform.forward;
		}
		if(Input.GetKey(this.MoveLeftKey)) {
			direction -= this.Ship.transform.right;
		}
		if(Input.GetKey(this.MoveRightKey)) {
			direction += this.Ship.transform.right;
		}
		if(Input.GetKey(this.MoveUpKey)) {
			direction += this.Ship.transform.up;
		}
		if(Input.GetKey(this.MoveDownKey)) {
			direction -= this.Ship.transform.up;
		}
		
		direction.Normalize();
		this.Ship.rigidbody.velocity += direction * acceleration;
		
		this.Camera.transform.position = this.Ship.transform.position;
		
		// Then handling rotation
		Quaternion shipRotation = this.Ship.transform.rotation;
		Quaternion camRotation = this.Camera.transform.rotation;
		float angle = Quaternion.Angle(camRotation, shipRotation);
		if(angle > this.RotationThresholdAngle)
		{
			this.Ship.rigidbody.MoveRotation(
				Quaternion.Slerp(shipRotation,
				                 camRotation,
				                 this.RotationPower * Time.deltaTime));
		}
	}
}
