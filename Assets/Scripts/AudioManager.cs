using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

//	public AudioClip backgroundMusic;
	public AudioClip[] towerPlaced;
	public AudioClip newWave;
	public AudioClip healthLost;
	public AudioClip mouseOverUi;
	public AudioClip lastHealth;
	public AudioClip bossSpawn;
	public AudioClip bossDead;
	public AudioClip gameOver;

	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource> ();

		/* Play background music. */
//		audioSource.PlayOneShot (backgroundMusic);
	}

	public void NewWave() {
		audioSource.PlayOneShot (newWave);
	}

	public void TowerPlaced() {
		AudioClip clip = towerPlaced[Random.Range (0, towerPlaced.Length - 1)];
		audioSource.PlayOneShot (clip);
	}

	public void HealthLost() {
		audioSource.PlayOneShot (healthLost);
	}

	public void MouseOverUi() {
		audioSource.PlayOneShot (mouseOverUi);
	}

	public void LastHealth() {
		audioSource.PlayOneShot (lastHealth);
	}

	public void BossSpawn() {
		audioSource.PlayOneShot (bossSpawn);
	}

	public void BossDead() {
		audioSource.PlayOneShot (bossDead);
	}

	public void GameOver() {
		audioSource.PlayOneShot (gameOver);
	}
}
