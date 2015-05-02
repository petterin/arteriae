using UnityEngine;
using System.Collections;

public class ShipHand : MonoBehaviour {
	public KeyCode control;
	
	private bool closed;

	// Use this for initialization
	void Start () {
		this.closed = false;
	}
	
	// Update is called once per frame
	void Update () {
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
