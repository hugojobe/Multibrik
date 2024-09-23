using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TonneauExplosionVFXBehaviour : PlayParticles
{
    [SerializeField] private Renderer explosionMeshRenderer;
    private MaterialPropertyBlock explosionMeshMPB;
    private float explosionMeshActivation = 0;
    [SerializeField] private  float explosionMeshActivationSpeed = 1.0f;
    private bool _explosionMeshIsActive = false;
    void Start()
    {
        explosionMeshMPB = new MaterialPropertyBlock();
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
            explosionMeshRenderer.SetPropertyBlock(explosionMeshMPB);
            explosionMeshMPB.SetFloat("_Activation", explosionMeshActivation);

            if (explosionMeshActivation == 1)
            {
                _explosionMeshIsActive = false;
            }
        }
    }
    public void StartTonneauVFX()
    {
        StartParticles();
        explosionMeshActivation = 0;
        _explosionMeshIsActive = true;
        
    }
}
