using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class FatBlobHandle : Destroyable {
	public ShipHand[] hands;
	
	private ShipHand collidingHand;
	private bool attached;
	private bool beingDragged;
	private Vector3 lastHandPosition;
	
	public bool IsAttached() {
		return this.attached;
	}

	// Use this for initialization
	void Start () {
		base.Start();
		this.attached = true;
		this.collidingHand = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.collidingHand != null && this.collidingHand.IsClosed()) {
			if(this.beingDragged) {
				this.transform.transform.position += this.collidingHand.transform.position - this.lastHandPosition;
			} else {
				this.beingDragged = true;
			}
			this.lastHandPosition = this.collidingHand.transform.position;
		} else {
			if(this.collidingHand != null && !this.collidingHand.IsClosed() && this.beingDragged && !this.attached) {
				this.StartDestroying();
			}
			this.beingDragged = false;
		}
		base.Update();
	}
	
	void OnTriggerEnter(Collider collider) {
		for(int i = 0; i < this.hands.Length; i++) {
			if(collider.Equals(this.hands[i].GetComponent<Collider>())) {
				Debug.Log("Collision enter with hand " + this.hands[i]);
				this.collidingHand = this.hands[i];
			}
		}
	}
	
	void OnTriggerExit(Collider collider) {
		// When the blob exits the wall, remove all movement constraints
		if(collider.name == "veinRoute") {
			Debug.Log("Handle blob exited wall!");
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			this.attached = false;
		}
		for(int i = 0; i < this.hands.Length; i++) {
			if(collider.Equals(this.hands[i])) {
				Debug.Log("Collision exit with hand " + this.hands[i]);
				this.collidingHand = null;
			}
		}
	}
}
