using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSounds : MonoBehaviour
{
    [SerializeField] private AudioSource randomSound;
    [SerializeField] private AudioClip[] sounds;
    void Start()
    {
        randomSound = GetComponent<AudioSource>();
        PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySound()
    {
        randomSound.clip = sounds[Random.Range(0, sounds.Length)];
        randomSound.Play();
    }
}
