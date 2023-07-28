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

    private void PlaySound()
    {
        randomSound.clip = sounds[Random.Range(0, sounds.Length)];
        randomSound.Play();
    }
}
