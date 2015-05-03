using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public AudioSource Source;
	public GameObject Player;
	private bool playedOnce;

	// Use this for initialization
	void Start () {
		playedOnce = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == Player)
		{
			if(!Source.isPlaying || !playedOnce){
				Source.Play();
				playedOnce = true;
				StartCoroutine(WaitAndRestart());

			}
				
		}
	}

	IEnumerator WaitAndRestart() {
		yield return new WaitForSeconds(Source.clip.length + 3);
		Application.LoadLevel(Application.loadedLevel);
	}
}
