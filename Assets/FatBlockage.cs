using UnityEngine;
using System.Collections;

public class FatBlockage : MonoBehaviour {	
	public FatBlob[] staticBlobs;
	public FatBlobHandle[] handleBlobs;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool anyHandleUnAttached = false;
		foreach(FatBlobHandle handleBlob in this.handleBlobs) {
			if(!handleBlob.IsAttached()) {
				anyHandleUnAttached = true;
			}
		}
		if(anyHandleUnAttached) {
			foreach(FatBlob staticBlob in this.staticBlobs) {
				if(staticBlob != null)
					staticBlob.Detatch();
			}
		}
	}
}
