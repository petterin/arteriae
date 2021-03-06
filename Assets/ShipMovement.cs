﻿using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	// The ship gives the movement it's direction
	public GameObject Ship;
	// This camera is translated relative to the ship
	public GameObject Camera;
	// Razer hydra contoller object, if available
	public GameObject SixenseInputObject;
	// Determines how much the velocity changes with respect to time passed
	public float Power = 1;
	// Determines how much the rotation changes with respect to time passed
	public float RotationPower = 1;
	// Determines how much the head must be turned to turn the ship
	public float RotationThresholdAngle = 25;
	
	public bool EnableRoll = true;
	public bool StartPaused = true;
	public KeyCode PauseKey = KeyCode.X;
	
	public Vector3 centerOfMass;
	
	private bool paused;

	private bool hasStarted = false;

	private KeyCode MoveForwardKey = KeyCode.W;
	private KeyCode MoveBackwardKey = KeyCode.S;
	private KeyCode MoveLeftKey = KeyCode.A;
	private KeyCode MoveRightKey = KeyCode.D;
	private KeyCode MoveUpKey = KeyCode.I;
	private KeyCode MoveDownKey = KeyCode.K;
	private KeyCode RollLeftKey = KeyCode.J;
	private KeyCode RollRightKey = KeyCode.L;

	// Use this for initialization
	void Start () {
		if(!this.Ship) {
			throw new UnityException("No ship detected in ShipMovement!");
		}
		if(!this.Camera) {
			throw new UnityException("No camera detected in ShipMovement!");
		}
		if(this.SixenseInputObject == null) {
			Debug.LogWarning("No SixenseInput found for the ShipMovement script! Only keyboard commands are available: WASD, IKJL", this);
		}
		this.Ship.GetComponent<Rigidbody>().centerOfMass = this.centerOfMass;
		this.paused = this.StartPaused;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!this.paused) {
			Vector3 direction = new Vector3(0,0,0);
			// Handling translation first
			if(this.SixenseInputObject != null && this.SixenseInputObject.activeInHierarchy) {
				// The left joystick is used for movement in the xz plane
				SixenseInput.Controller leftController = SixenseInput.GetController(SixenseHands.LEFT);
				// Moving up and down is controlled by the right handle's buttons 4 and 3
				SixenseInput.Controller rightController = SixenseInput.GetController(SixenseHands.RIGHT);
				
				if(leftController == null || rightController == null) {
					Debug.Log("Could not find Sixense controllers!");
				} else {		
					direction = this.Ship.transform.forward * leftController.JoystickY +
						this.Ship.transform.right * leftController.JoystickX;
					
					direction += this.Ship.transform.up * rightController.JoystickY;
					if(this.EnableRoll) {
						Vector3 torque = this.Ship.transform.forward * 
							this.RotationPower * 
							-rightController.JoystickX;
						this.Ship.GetComponent<Rigidbody>().AddTorque(getTorqueWithoutInertiaTensor(torque));
					}
				}
			} else {
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
				if(this.EnableRoll) {
					Vector3 torque = new Vector3(0,0,0);
					if(Input.GetKey(this.RollLeftKey)) {
						torque += this.Ship.transform.forward * this.RotationPower;
					}
					if(Input.GetKey(this.RollRightKey)) {
						torque -= this.Ship.transform.forward * this.RotationPower;
					}
					this.Ship.GetComponent<Rigidbody>().AddTorque(getTorqueWithoutInertiaTensor(torque));
				}
			}
			
			direction.Normalize();
			// Note: no deltaTime multiplier is necessary, the physics engine takes care of that
			Rigidbody rigidbody = this.Ship.GetComponent<Rigidbody>();
			rigidbody.AddForce(direction * this.Power);
			
			// Moving the camera with the ship
			// Actually, rely on the oculus + kinect to get the position right
			// this.Camera.transform.position = this.Ship.transform.position;
			
			// Then handling rotation
			float angle = Vector3.Angle(this.Ship.transform.forward, this.Camera.transform.forward);
			if(angle > this.RotationThresholdAngle) {
				// Rotation axis is the cross product of the target and current forward vector
				Vector3 axis = Vector3.Cross(this.Ship.transform.forward, this.Camera.transform.forward);
				Vector3 torque = getTorqueWithoutInertiaTensor(axis * this.RotationPower);
				this.Ship.GetComponent<Rigidbody>().AddTorque(torque);
			}
		}
	}
	
	void Update() {
		if(Input.GetKeyDown(this.PauseKey)) {
			this.paused = !this.paused;
			Debug.Log("Set paused: " + this.paused);
			if (this.hasStarted == false && this.paused == false) {
				this.hasStarted = true;
				Debug.Log ("Pause was taken off for the first time. LET'S GO!!");
				this.GetComponent<AudioSource>().Play();
			}
		}
	}
	
	private Vector3 getTorqueWithoutInertiaTensor(Vector3 torque) {
		Rigidbody rigidbody = this.Ship.GetComponent<Rigidbody>();
		Quaternion q = this.Ship.transform.rotation * rigidbody.inertiaTensorRotation;
		return q * Vector3.Scale(rigidbody.inertiaTensor, Quaternion.Inverse(q) * torque);
	}
}
