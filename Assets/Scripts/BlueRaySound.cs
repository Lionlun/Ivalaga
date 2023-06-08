using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueRaySound : MonoBehaviour
{	
	private AudioSource sound;
	[SerializeField] private AudioClip soundClip;
	void Start()
    {
        sound = GetComponent<AudioSource>();
		PlaySound();
	}

	void PlaySound()
	{
		sound.clip = soundClip;
		sound.Play();
	}
}
