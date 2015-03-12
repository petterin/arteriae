using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

	public GameObject TargetCamera;
	public float RotationSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(this.TargetCamera) {
			this.transform.rotation =
				Quaternion.Slerp(this.transform.rotation,
								 this.TargetCamera.transform.rotation,
								 this.RotationSpeed);
		}
	}
}
