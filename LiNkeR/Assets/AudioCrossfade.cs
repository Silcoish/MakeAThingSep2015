using UnityEngine;
using System.Collections;

public class AudioCrossfade : MonoBehaviour {

    public AudioSource current;
    public AudioSource prev;

    public float maxVol;
    public float minVol;

    void Start()
    {
        prev.volume = 0;
    }

    void SwapSound(AudioClip clip)
    {
        prev.clip = current.clip;
        current.clip = clip;
        prev.volume = current.volume;
        current.volume = 0;
    }
}
