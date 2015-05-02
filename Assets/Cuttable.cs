using UnityEngine;
using System.Collections;

public class Cuttable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col)
	{
		Debug.Log ("Triggered by " + col.gameObject.name);
		if(col.gameObject.name.StartsWith("Cutter"))
		{
			Destroy(this.gameObject);
		}
	}
}
