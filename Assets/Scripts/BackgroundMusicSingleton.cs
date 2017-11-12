using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicSingleton : MonoBehaviour {

	private static BackgroundMusicSingleton instance = null;

	public static BackgroundMusicSingleton Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			if (instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip) {
				instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
				instance.GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume;
				instance.GetComponent<AudioSource>().Play ();
			}

			Destroy (this.gameObject);
			return;
		}

		instance = this;
		GetComponent<AudioSource>().Play ();
		DontDestroyOnLoad (this.gameObject);
	}
}
