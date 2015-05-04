using UnityEngine;
using System.Collections;

public class ShipHand : MonoBehaviour {
	// Used only to detect trigger presses
	public SixenseHands Hand;
	// Used for debugging mainly
	public KeyCode Control;
	public Collider ClawCollider;
	
	public GameObject ClawLeft;
	public GameObject ClawRight;
	public Vector3 ClawPivotLeft;
	public Vector3 ClawPivotRight;
	
	public float CloseSpeed = 10.0f;
	public float MaxAngleOfClaws;
	
	private bool closed;
	
	private float currentRotation;

	// Use this for initialization
	void Start () {
		this.closed = false;
	}
	
	// Update is called once per frame
	void Update () {
		SixenseInput.Controller controller = SixenseInput.GetController(Hand);
		
		Vector3 pivotLeft = this.ClawLeft.transform.TransformPoint(this.ClawPivotLeft);
		Vector3 pivotRight = this.ClawRight.transform.TransformPoint(this.ClawPivotRight);
		
		float angle = Time.deltaTime * this.CloseSpeed;
		
		// NOTE: the trigger button threshold can be set through SixenseInput
		if(Input.GetKey(this.Control) || (controller != null && controller.GetButton(SixenseButtons.TRIGGER))) {
			if(!this.closed)
				Debug.Log("Closing hand");
			this.closed = true;
			if(this.currentRotation + angle <= this.MaxAngleOfClaws) {
				this.currentRotation += angle;
			} else {
				angle = this.MaxAngleOfClaws - this.currentRotation;
				this.currentRotation = this.MaxAngleOfClaws;
			}
			this.ClawLeft.transform.RotateAround(pivotLeft, this.ClawLeft.transform.up, angle);
			this.ClawRight.transform.RotateAround(pivotRight, this.ClawRight.transform.up, -angle);
		} else {
			if(this.closed)
				Debug.Log("Opening hand");
			this.closed = false;
			if(this.currentRotation - angle >= 0) {
				this.currentRotation -= angle;
			} else {
				angle = this.currentRotation;
				this.currentRotation = 0;
			}
			this.ClawLeft.transform.RotateAround(pivotLeft, this.ClawLeft.transform.up, -angle);
			this.ClawRight.transform.RotateAround(pivotRight, this.ClawRight.transform.up, angle);
		}
	}
	
	public bool IsClosed() {
		return this.closed;
	}
	/*
	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(ClawLeft.transform.TransformPoint(this.ClawPivotLeft), 1.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(ClawRight.transform.TransformPoint(this.ClawPivotRight), 1.5f);
	}
	*/
}
