using UnityEngine;
using System.Collections;

public class MoveSound : MonoBehaviour {
	
	private bool isMoving;
	private LimitedQueue<Quaternion> lastRotations;
	public AudioClip MoveStartSound;
	public AudioClip MoveInProgressSound;
	public AudioClip MoveEndSound;
	public AudioSource AudioS;
	public float AudioVolume = 1.0f;

	// Use this for initialization
	void Start () {
		isMoving = false;
		lastRotations = new LimitedQueue<Quaternion> (10);
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion currentRotation = this.transform.rotation;
		lastRotations.Enqueue (currentRotation);
		if (stoppingMove (isMoving, lastRotations.Peek(), currentRotation)) {
			isMoving = false;
			AudioS.loop = false;
			AudioS.Pause();
			AudioS.PlayOneShot(MoveEndSound, AudioVolume);

		}
		else if (startingMove (isMoving, lastRotations.Peek(), currentRotation)) {
			isMoving = true;
			AudioS.PlayOneShot(MoveStartSound, AudioVolume);
			StartCoroutine(startMoveInProgressAfterStartSound());
		}
	}

	IEnumerator startMoveInProgressAfterStartSound() {
		yield return new WaitForSeconds(MoveStartSound.length);
		AudioS.loop = true;
		AudioS.clip = MoveInProgressSound;
		AudioS.Play();
	}


	bool stoppingMove(bool isMoving, Quaternion rotationA, Quaternion rotationB) {
		return isMoving && rotationA == rotationB;	
	}

	bool startingMove(bool isMoving, Quaternion rotationA, Quaternion rotationB) {
		return !isMoving && rotationA != rotationB;	
	}

}
