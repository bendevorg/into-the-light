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

	public bool isFadeBugged;

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
		if(!isFadeBugged){
			StopCoroutine(FadeOut());
			StartCoroutine(FadeIn());
		} else {
			audioSource.Play();
		}
	}

	public void Stop(){
		if (playing){
			playing = false;
			if (!isFadeBugged){
				StopCoroutine(FadeIn());
				StartCoroutine(FadeOut());
			} else {
				audioSource.Stop();
			}
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
