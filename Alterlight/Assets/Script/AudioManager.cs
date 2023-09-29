using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip Slash;
    public AudioClip PlayerHit;
    public AudioClip Hit;
    public AudioClip PlayerDeath;
    public AudioClip EnemyDeath;
    public AudioClip Healing;
    public AudioClip HealthPickUp;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
