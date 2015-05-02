using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class FatBlob : Destroyable
{
	private bool attached;
	
	void Start() {
		base.Start();
		this.attached = true;
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}
	
	public void Detatch() {
		//Debug.Log("Detatching blob " + this + "!");
		this.attached = false;
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		this.StartDestroying();
	}
}

