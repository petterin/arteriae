using UnityEngine;
using System.Collections;

public class ArmControl : MonoBehaviour {

	public Transform ShipTransform;
	public Transform HandTransform;
	public Vector3 ShoulderPosition;
	public Vector3 WristPosition;
	
	// Note: The arm must not be rotated in the world space!
	public GameObject UpperArm;
	// Note: The arm must not be rotated in the world space!
	public GameObject Forearm;
	
	private Vector3 forearmSize;
	private Vector3 upperArmSize;

	// Use this for initialization
	void Start () {
		// Note: if the arm is rotated in world space, this computation will give the wrong result
		this.upperArmSize = this.UpperArm.GetComponent<Renderer>().bounds.size;
		this.forearmSize = this.Forearm.GetComponent<Renderer>().bounds.size;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 wristTargetInWorld = this.HandTransform.TransformPoint(this.WristPosition);
		Vector3 shoulderTargetInWorld = this.ShipTransform.TransformPoint(this.ShoulderPosition);
		Vector3 armInWorld = wristTargetInWorld - shoulderTargetInWorld;
		Vector3 armDirection = armInWorld.normalized;
		
		// Moving and rotating the forearm, no scaling for that
		Vector3 forearmOrigin = shoulderTargetInWorld + armInWorld - armDirection * this.forearmSize.z;
		this.Forearm.transform.position = forearmOrigin;
		Vector3 forearmRotationInLocal = this.Forearm.transform.InverseTransformVector(this.Forearm.transform.forward);
		this.Forearm.transform.rotation = Quaternion.FromToRotation(forearmRotationInLocal, armDirection);
		
		// Then the upper arm, which is scaled to fill the gap between the ship and the forearm
		Vector3 upperArmInWorld = forearmOrigin - shoulderTargetInWorld;
		this.UpperArm.transform.position = shoulderTargetInWorld;
		Vector3 upperArmRotationInLocal = this.UpperArm.transform.InverseTransformVector(this.UpperArm.transform.forward);
		this.UpperArm.transform.rotation = Quaternion.FromToRotation(upperArmRotationInLocal, armDirection);
		// Scaling only the Z axis
		Vector3 upperArmScale = this.UpperArm.transform.localScale;
		upperArmScale.z = upperArmInWorld.magnitude / this.upperArmSize.z;
		this.UpperArm.transform.localScale = upperArmScale;
	}
}
