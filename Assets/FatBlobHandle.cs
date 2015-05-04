using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class FatBlobHandle : Destroyable {
	public ShipHand[] hands;
	public AudioSource completionPlayer;
	public AudioSource completionVoicePlayer;
	private ShipHand collidingHand;
	private bool attached;
	private bool beingDragged;
	private Vector3 lastHandPosition;
	private AudioSource slimePlayer;


	public bool IsAttached() {
		return this.attached;
	}

	// Use this for initialization
	void Start () {
		base.Start();
		this.attached = true;
		this.collidingHand = null;
		this.slimePlayer = this.transform.parent.gameObject.GetComponent<AudioSource>();
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
			// The blob was dragged off and the hand was released
			if(this.collidingHand != null && !this.collidingHand.IsClosed() && this.beingDragged && !this.attached) {
				this.StartDestroying();
			}
			this.beingDragged = false;
		}
		base.Update();
	}
	
	void OnTriggerEnter(Collider collider) {
		for(int i = 0; i < this.hands.Length; i++) {
			if(collider.Equals(this.hands[i].ClawCollider)) {
				Debug.Log("Collision enter with hand " + this.hands[i]);
				this.collidingHand = this.hands[i];
				if (!this.slimePlayer.isPlaying) {
					this.slimePlayer.Play();
					Debug.Log ("Slime playing");
				}
			}
		}
	}
	
	void OnTriggerExit(Collider collider) {
		// When the blob exits the wall, remove all movement constraints
		if(collider.name == "veinRoute") {
			Debug.Log("Handle blob exited wall!");
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			this.attached = false;
			completionPlayer.PlayOneShot (completionPlayer.clip, 1f);
			if(completionVoicePlayer != null) {
				completionVoicePlayer.PlayDelayed(1f);
			}
		}
		for(int i = 0; i < this.hands.Length; i++) {
			if(collider.Equals(this.hands[i])) {
				Debug.Log("Collision exit with hand " + this.hands[i]);
				this.collidingHand = null;
				if (this.slimePlayer.isPlaying) {
					this.slimePlayer.Stop();
					Debug.Log ("SLIME STOPPED");
				}
			}
		}
	}
}
