using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{

    [SerializeField]
    private Material colorCorrectionMat;
    [SerializeField]
    private AudioMixer masterMixer;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer vfxMixer;


    private float volumeSteps = 8.0f;
    private const float MINVOLUME = -80f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMasterVolume(float newvolume){
        masterMixer.SetFloat("Volume", newvolume * volumeSteps + MINVOLUME);
    }

    public void ChangeMusicVolume(float newvolume){
        musicMixer.SetFloat("Volume", newvolume * volumeSteps + MINVOLUME);
    }

     public void ChangeVFXVolume(float newvolume){
        vfxMixer.SetFloat("Volume", newvolume * volumeSteps + MINVOLUME);
    }

    public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
