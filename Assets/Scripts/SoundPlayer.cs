using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> soundsToPlay;
    [SerializeField]
    private float minPitch = -3f, maxPitch = 3f;
    private float currentPitch = 1f;
    private AudioSource emitter;


    public void Play(){
        int soundID = Random.Range(0, soundsToPlay.Count);
        currentPitch = Random.Range(minPitch, maxPitch);
        emitter.pitch = currentPitch;
        emitter.clip = soundsToPlay[soundID];
        emitter.Play();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        if(TryGetComponent<AudioSource>(out emitter)){
            Play();
        }
    }

}
