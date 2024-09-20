using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSplashBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem _beerParticle01;
    [SerializeField] private ParticleSystem _beerParticle02;
    [SerializeField] private ParticleSystem _beerParticle03;
    [SerializeField] private bool _play = false;

    void Update()
    {
        if (_play)
        {
            PlayBeerSplash();
            _play = false;
        }
    }
    public void PlayBeerSplash()
    {
        _beerParticle01.Play();
        _beerParticle02.Play();
        _beerParticle03.Play();
    }
}
