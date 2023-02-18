using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }
	public AudioSource audioSource;
	public AudioClip fightSong;
	public AudioClip mainSong;

	public AudioClip[] clips = null;

	public AudioClip RandomClip()
	{
		return clips[Random.Range(0, clips.Length)];
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
    void Update()
    {
		if(!audioSource.isPlaying)
		{
			audioSource.clip = RandomClip();
			audioSource.Play();
		}
    }
}
