using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntro : MonoBehaviour
{
    public AudioSource MusicSource; //Source of Music
    public AudioClip MusicIntro; // Music Intro Clip

    void Start()
    {
        MusicSource.PlayOneShot(MusicIntro); //Plays music intro once
        MusicSource.PlayScheduled(AudioSettings.dspTime + MusicIntro.length); //Duration of music intro
    }
}
