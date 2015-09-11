using UnityEngine;
using System.Collections;

public class AudioCrossfade : MonoBehaviour {

    public AudioSource current;
    public AudioSource prev;

    public float maxVol;
    public float minVol;

    bool needsEnable = false;
    float globalTime = 0;

    float volMovementPerSecond = 0.5f;

    void Start()
    {
        prev.volume = 0;
    }

    void Update()
    {
        if(needsEnable)
        {
            prev.enabled = true;
            prev.time = globalTime;
            current.time = globalTime;
            current.enabled = true;
            needsEnable = false;
        }
        prev.volume -= volMovementPerSecond * Time.deltaTime;
        current.volume += volMovementPerSecond * Time.deltaTime;

        if (prev.volume < 0)
            prev.volume = 0;
        if (current.volume > 1.0f)
            current.volume = 1.0f;
    }

    public void SwapSound(AudioClip clip)
    {
        prev.clip = current.clip;
        globalTime = current.time;
        current.clip = clip;
        prev.volume = current.volume;
        current.volume = 0;
        prev.enabled = false;
        current.enabled = false;
        needsEnable = true;
    }
}
