using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private bool playOnAwake = false;
    [SerializeField]
    private List<AudioClip> soundsToPlay;
    [SerializeField]
    private float minPitch = -3f, maxPitch = 3f;
    private float currentPitch = 1f;
    [SerializeField]
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
        if(emitter == null && !TryGetComponent<AudioSource>(out emitter)){
            this.enabled = false;
            return;
        }

        if(playOnAwake){
            Play();
        }
        
    }



}
