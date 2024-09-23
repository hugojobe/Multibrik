using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonneauTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] public bool start = false;
    [SerializeField] private bool end = false;
    [SerializeField] private float activationSpeedUp = 2.0f;
    [SerializeField] private float activationSpeedDown = 2.0f;
    [SerializeField] private float trailDuration = 2.0f;
    [SerializeField] private Renderer trailMat;
    private MaterialPropertyBlock trailMPB;
    private bool trailIsActive = false;
    private float trailActivation = 0;
    private void Start()
    {
        trailMPB = new MaterialPropertyBlock();
    }
    void Update()
    {
        if (start)
        {
            StartTrail();
            start = false;
        }
        if (end)
        {
            StopTrail();
            end = false;
            
        }
        if (trailIsActive)
        {
            trailActivation = Mathf.MoveTowards(trailActivation, 1, activationSpeedUp * Time.deltaTime);
        }
        else
        {
            trailActivation = Mathf.MoveTowards(trailActivation, 0, activationSpeedDown * Time.deltaTime);
        }
        trailMat.SetPropertyBlock(trailMPB);
        trailMPB.SetFloat("_Activation", trailActivation);
    }
    public void StartTrail()
    {
        StartCoroutine(TrailTime());
        trailIsActive = true;      
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
    }
    public void StopTrail()
    {
        trailIsActive = false;
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
        }
    }
    private IEnumerator TrailTime()
    {
        yield return new WaitForSeconds(trailDuration);
        StopTrail();
    }
}
