using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonneauExplosifBallImpact : MonoBehaviour
{
    private Vector3 lastPos;
    [SerializeField] private ParticleSystem[] particles;

    public void StartParticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }

    }
    public void BallImpact()
    {
        lastPos = transform.position;
        StartCoroutine(PlayParticleDelay());
    }
    private IEnumerator PlayParticleDelay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.LookAt(lastPos);
        transform.position = lastPos;
        StartParticles();
    }
}
