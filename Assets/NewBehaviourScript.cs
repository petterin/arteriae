using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {
	private bool destroying;
	private Vector3 originalScale;
	private float shrinkSpeed;

	// Use this for initialization
	protected void Start () {
		this.destroying = false;
		this.originalScale = this.transform.localScale;
		this.shrinkSpeed = 20 + Random.Range(-10f, 10f);
	}
	
	// Update is called once per frame
	protected void Update () {
		if(this.destroying) {
			float scale = this.shrinkSpeed * Time.deltaTime;
			Debug.Log("Scaling down with " + scale + "shrink speed: " + this.shrinkSpeed + " dT: " + Time.deltaTime);
			this.transform.localScale -= new Vector3(scale,scale,scale);
			if(this.transform.localScale.magnitude <= (this.originalScale * 0.1f).magnitude) {
				Debug.Log ("Destroying " + this);
				Destroy(this.gameObject);
			}
		}
	}
	
	public void StartDestroying() {
		Debug.Log ("Starting to destroy " + this);
		this.destroying = true;
	}
}
