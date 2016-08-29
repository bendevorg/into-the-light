using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

	[HideInInspector]
	public AudioSource audioSource;

	public float fadeInTime;
	public float fadeOutTime;

	public float maxVolume;

	public bool playing;

	bool fade = false;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		if (playing){
			Play();
		}
	}

	void Update(){

		/*if (Input.GetKey(KeyCode.A)){
			Play();
		} else if(Input.GetKey(KeyCode.D)){
			Stop();
		}*/

	}

	public void Play(){
		playing = true;
		StartCoroutine(FadeIn());
	}

	public void Stop(){
		if (playing){
			playing = false;
			StartCoroutine(FadeOut());
		}
	}

	IEnumerator FadeOut () {

        while (audioSource.volume > 0.1f) {
			audioSource.volume = Mathf.Lerp(audioSource.volume, 0, fadeOutTime);
            yield return null;
        }

        audioSource.Stop ();
        audioSource.volume = 0;
 
    }

	IEnumerator FadeIn(){

		audioSource.volume = 0;
		audioSource.Play();
 
        while (audioSource.volume < maxVolume) {
            audioSource.volume +=  Time.deltaTime / fadeInTime;
 
            yield return null;
        }

	}

}
