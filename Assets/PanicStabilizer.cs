using UnityEngine;
using System.Collections;

public class PanicStabilizer : MonoBehaviour {
	public KeyCode Control;
	public GameObject shipModel;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(this.Control)) {
			Debug.Log (this.gameObject.transform.rotation);
			this.shipModel.transform.rotation = Quaternion.identity;

		}
	
	}
}
