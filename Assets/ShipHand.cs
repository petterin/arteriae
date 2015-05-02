using UnityEngine;
using System.Collections;

public class ShipHand : MonoBehaviour {
	// Used only to detect trigger presses
	public SixenseHands Hand;
	// Used for debugging mainly
	public KeyCode Control;
	
	private bool closed;

	// Use this for initialization
	void Start () {
		this.closed = false;
	}
	
	// Update is called once per frame
	void Update () {
		SixenseInput.Controller controller = SixenseInput.GetController(Hand);
		// NOTE: the trigger button threshold can be set through SixenseInput
		if(Input.GetKey(this.Control) || (controller != null && controller.GetButton(SixenseButtons.TRIGGER))) {
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
