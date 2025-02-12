using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musiceSource;
    [SerializeField] AudioSource SFXSource;
    [Header("----- Audio Clip -----")]  
    public AudioClip background;
    public AudioClip death;
    public AudioClip monsterSounds;
    public AudioClip objectBroken;
    public AudioClip weaponSwing;
    public AudioClip hitEnemy;
    public AudioClip Falling;
    public AudioClip Collect;
    public AudioClip caveSound1;
    public AudioClip caveSound2;
    public AudioClip caveSound3;
    private void Start()
    {
        musiceSource.clip = background;
        musiceSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
