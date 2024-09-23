using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBlock : MonoBehaviour
{
    public int ballSpeedMultiplier;

    public float explosionRadius;

    [SerializeField] private PlayParticles windVFX;
    [SerializeField] private TonneauExplosionVFXBehaviour explosionVFX;
    [SerializeField] private TonneauTrail tonneauTrail;
    [SerializeField] private TonneauExplosifBallImpact tonneauExplosifBallImpact;
    [SerializeField] private SoundPlayer ExplosionSound;
    [SerializeField] private float destroyDelay = 1.5f;
    [SerializeField] private GameObject TonneauRenderer;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Ground")) {
            Debug.Log("hit");
            windVFX.StartParticles();
            
            // VFX HIT GROUND
        }
    }

    public void StartExplosionSequence() {

        // HIT
        tonneauTrail.StartTrail();
        tonneauExplosifBallImpact.BallImpact();

        Invoke("Explode", 2);
    }

    private void Explode() {

        // EXPLOSIONN FEEDBACK
        explosionVFX.StartTonneauVFX();
        ExplosionSound.Play();
        TonneauRenderer.SetActive(false);
        StartCoroutine(DestroyTime());

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders) {
            Block block = collider.GetComponent<Block>();
            if(block != null) {
                block.TakeDamage(1, Vector3.zero, Vector3.zero);
            }
        }
       
    }
    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
