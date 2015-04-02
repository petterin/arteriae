using UnityEngine;
using System.Collections;

public class audioSwitcher : MonoBehaviour {


	AudioSource source;
	public AudioClip fastHeartBeatClip;
	public int changeHeartBeatInSeconds = 5;
	// Use this for initialization
	void Start () {
		source = GetComponents<AudioSource> () [1];
		StartCoroutine(changeHeartBeatSpeed());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator changeHeartBeatSpeed() {
		yield return new WaitForSeconds(changeHeartBeatInSeconds);
		source.Pause ();
		source.clip = fastHeartBeatClip;
		source.Play ();
	}
}
