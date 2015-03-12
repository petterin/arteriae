using UnityEngine;
using System.Collections;

// Movement for a ship rigidbody. Bases the movement on the ship's, and not the camera's, rotation.
[RequireComponent (typeof (Rigidbody))]
public class ShipMovement : MonoBehaviour {
	// The ship gives the movement it's direction
	public GameObject Ship;
	// Determines how much the velocity changes in each frame
	public float Power;
	
	public KeyCode MoveForwardKey = KeyCode.W;
	public KeyCode MoveBackwardKey = KeyCode.S;
	public KeyCode MoveLeftKey = KeyCode.A;
	public KeyCode MoveRightKey = KeyCode.D;
	
	// Use this for initialization
	void Start () {
		if(!this.Ship) {
			throw new UnityException("No ship detected in ShipMovement!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(this.Ship) {
			float acceleration = Time.deltaTime * this.Power;
			Vector3 direction = new Vector3(0,0,0);
			
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
			
			direction.Normalize();
			this.rigidbody.velocity += direction * acceleration;
		}
	}
}
