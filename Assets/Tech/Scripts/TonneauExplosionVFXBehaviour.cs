using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TonneauExplosionVFXBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle01;
    [SerializeField] private ParticleSystem particle02;
    [SerializeField] private Material tonneauExplosionMeshMat;
    [SerializeField] private bool start = false;
    private float explosionMeshActivation = 0;
    [SerializeField] private  float explosionMeshActivationSpeed = 1.0f;
    private bool _explosionMeshIsActive = false;
    void Start()
    {
        explosionMeshActivation = 0;
    }   
    void Update()
    {
        if (start)
        {
            StartTonneauVFX();
            start = false;
        }
        if (_explosionMeshIsActive)
        {
            explosionMeshActivation = Mathf.MoveTowards(explosionMeshActivation, 1, explosionMeshActivationSpeed * Time.deltaTime);
            tonneauExplosionMeshMat.SetFloat("_Activation", explosionMeshActivation);
            if (explosionMeshActivation == 1)
            {
                _explosionMeshIsActive = false;
            }
        }
    }
    public void StartTonneauVFX()
    {
        particle01.Play();
        particle02.Play();
        explosionMeshActivation = 0;
        _explosionMeshIsActive = true;
        
    }
}
