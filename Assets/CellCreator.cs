using UnityEngine;
using System.Collections;

public class CellCreator : MonoBehaviour {

	public GameObject cell;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 0, 3f);
		
	}
	void Spawn () {
		GameObject obj = (GameObject) Instantiate (cell, transform.position, Random.rotation);
		Vector3 initialVelocity = Random.insideUnitSphere * 10;
		obj.GetComponent<Rigidbody>().velocity = initialVelocity;
	}

}
