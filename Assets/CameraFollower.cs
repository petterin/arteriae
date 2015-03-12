using UnityEngine;
using System.Collections;

// Follows the target GameObject's (camera's) rotation with the given rotation speed. 
public class CameraFollower : MonoBehaviour {

	public GameObject TargetCamera;
	public float RotationSpeed = 1.0f;
	public int RotationThresholdAngle = 1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(this.TargetCamera) {
			if(Quaternion.Angle(this.transform.rotation, this.TargetCamera.transform.rotation) > RotationThresholdAngle){
				this.transform.rotation =
					Quaternion.Slerp(this.transform.rotation,
					                 this.TargetCamera.transform.rotation,
					                 this.RotationSpeed * Time.deltaTime);
			}
		}
	}
}
