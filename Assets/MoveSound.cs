using UnityEngine;
using System.Collections;

public class MoveSound : MonoBehaviour {
	
	private bool isMoving;
	private bool fadingOut;
	private LimitedQueue<Vector3> lastLocations;
	public AudioClip MoveInProgressSound;
	public AudioSource AudioS;
	public float AudioVolume = 1.0f;
	private double locationThreshold = 0.3;
	
	// Use this for initialization
	void Start () {
		isMoving = false;
		lastLocations = new LimitedQueue<Vector3> (10);
		lastLocations.Enqueue (this.transform.position);
	}
	
	// Update is called once per frame

	void Update () {
		Vector3 currentLocation = this.transform.position;
		lastLocations.Enqueue (currentLocation);
		if (stoppingMove (isMoving, lastLocations.Peek(), currentLocation)) {
			isMoving = false;
			fadingOut = true;
		}
		else if (startingMove (isMoving, lastLocations.Peek(), currentLocation)) {
			isMoving = true;
			fadingOut = false;
			AudioS.loop = true;
			AudioS.clip = MoveInProgressSound;
			AudioS.volume = AudioVolume;
			AudioS.Play();
		}

		if (fadingOut) {
			AudioS.volume = AudioS.volume - 0.005f;
			
			if(AudioS.volume <= 0){
				AudioS.Pause();
				fadingOut = false;
			}
		}
	}
	

		
	bool stoppingMove(bool isMoving, Vector3 locationA, Vector3 locationB) {
		return isMoving && Vector3.Distance(locationA, locationB) <= locationThreshold;	
	}
	
	bool startingMove(bool isMoving, Vector3 locationA, Vector3 locationB) {
		return !isMoving && !fadingOut && Vector3.Distance(locationA, locationB) > 0.5;
	}
	
}
