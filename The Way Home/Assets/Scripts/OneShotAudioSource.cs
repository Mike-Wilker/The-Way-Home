using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAudioSource : MonoBehaviour
{
    public void setup(AudioClip clip, float volume, float pitch)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().volume = volume;
        GetComponent<AudioSource>().pitch = pitch;
        GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
