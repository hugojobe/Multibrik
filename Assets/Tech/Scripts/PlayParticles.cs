using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] public bool start = false;
    void Update()
    {
        if (start)
        {
            StartParticles();
            start = false;
        }
    }
    public void StartParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }

    }
}
