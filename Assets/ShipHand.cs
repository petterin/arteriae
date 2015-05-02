using UnityEngine;
using System.Collections;

public class ShipHand : MonoBehaviour {
	public KeyCode control;
	public GameObject parent;
	private Vector3 originalPosition;
	private Vector3 translationOffset;
	
	private bool closed;

	// Use this for initialization
	void Start () {
		this.closed = false;
		this.originalPosition = this.parent.transform.InverseTransformPoint(this.transform.position);
		//this.translationOffset = this.transform.position - this.parent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.parent != null) {
			this.transform.position = this.parent.transform.TransformPoint(this.originalPosition/* TODO: Razer hydra position delta */);
			//this.transform.position = this.translationOffset + this.parent.transform.position;
		}
		if(Input.GetKey(this.control)) {
			if(!this.closed)
				Debug.Log("Closing hand");
			this.closed = true;
		} else {
			if(this.closed)
				Debug.Log("Opening hand");
			this.closed = false;
		}
	}
	
	public bool IsClosed() {
		return this.closed;
	}
}
