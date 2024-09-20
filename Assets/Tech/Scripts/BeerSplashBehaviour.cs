using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSplashBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem _beerParticle01;
    [SerializeField] private ParticleSystem _beerParticle02;
    [SerializeField] private ParticleSystem _beerParticle03;
    [SerializeField] private bool _play = false;

    private void PlayParticle()
    {
        _beerParticle01.Play();
        _beerParticle02.Play();
        _beerParticle03.Play();
    }

    void Update()
    {
        if (_play)
        {
            _beerParticle01.Play();
            _beerParticle02.Play();
            _beerParticle03.Play();
            _play = false;
        }
    }
}
