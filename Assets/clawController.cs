using UnityEngine;
using System.Collections;

public class clawController : MonoBehaviour {

	public GameObject RightClawLeftBlade;
	public GameObject RightClawRightBlade;
	public GameObject LeftClawLeftBlade;
	public GameObject LeftClawRightBlade;
	public GameObject SixenseInputObject;
	private KeyCode RightClawClose = KeyCode.P;
	private KeyCode LeftClawClose = KeyCode.O;
	private float openCloseVelocity = 30f;
	private float closingForce = 20f;

	void Start () {
		if(this.SixenseInputObject == null) {
			Debug.LogWarning("No SixenseInput found for the clawController script! Close claws with: O, P", this);
		}
	}
	void FixedUpdate ()
	{	
		JointMotor rightClawlLeftMotor = new JointMotor();
		JointMotor rightClawRightMotor = new JointMotor();
		rightClawRightMotor.force = closingForce;
		rightClawlLeftMotor.force = closingForce;
		JointMotor leftClawlLeftMotor = new JointMotor();
		JointMotor leftClawRightMotor = new JointMotor();
		leftClawRightMotor.force = closingForce;
		leftClawlLeftMotor.force = closingForce;


		if (this.SixenseInputObject != null) {
			SixenseInput.Controller leftController = SixenseInput.GetController(SixenseHands.LEFT);
			SixenseInput.Controller rightController = SixenseInput.GetController(SixenseHands.RIGHT);
			
			if(leftController == null || rightController == null) {
				Debug.Log("Could not find Sixense controllers!");
			} else {
				if(leftController.Trigger > 0) {
					leftClawlLeftMotor.targetVelocity = -openCloseVelocity;
					leftClawRightMotor.targetVelocity = openCloseVelocity;
				}
				else {
					leftClawlLeftMotor.targetVelocity = openCloseVelocity;
					leftClawRightMotor.targetVelocity = -openCloseVelocity;
				}

				if(rightController.Trigger > 0) {
					rightClawlLeftMotor.targetVelocity = -openCloseVelocity;
					rightClawRightMotor.targetVelocity = openCloseVelocity;
				}
				else {
					rightClawlLeftMotor.targetVelocity = openCloseVelocity;
					rightClawRightMotor.targetVelocity = -openCloseVelocity;
				}

			}

			
		} else {
			if(Input.GetKey(this.LeftClawClose)) {
				leftClawlLeftMotor.targetVelocity = -openCloseVelocity;
				leftClawRightMotor.targetVelocity = openCloseVelocity;

				
			}
			else {
				leftClawlLeftMotor.targetVelocity = openCloseVelocity;
				leftClawRightMotor.targetVelocity = -openCloseVelocity;
			}

			if(Input.GetKey(this.RightClawClose)){
				rightClawlLeftMotor.targetVelocity = -openCloseVelocity;
				rightClawRightMotor.targetVelocity = openCloseVelocity;

			}
			else {
				rightClawlLeftMotor.targetVelocity = openCloseVelocity;
				rightClawRightMotor.targetVelocity = -openCloseVelocity;

			}
			
		}
		HingeJoint RightClawjointRight = RightClawRightBlade.GetComponent<HingeJoint>();
		HingeJoint RightClawjointLeft = RightClawLeftBlade.GetComponent<HingeJoint>();
		RightClawjointLeft.motor = rightClawlLeftMotor;
		RightClawjointRight.motor = rightClawRightMotor;

		HingeJoint LeftClawjointRight = LeftClawRightBlade.GetComponent<HingeJoint>();
		HingeJoint LeftClawjointLeft = LeftClawLeftBlade.GetComponent<HingeJoint>();
		LeftClawjointLeft.motor = leftClawlLeftMotor;
		LeftClawjointRight.motor = leftClawRightMotor;

	}
}
