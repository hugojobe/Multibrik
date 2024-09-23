using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBlock : MonoBehaviour
{
    public int ballSpeedMultiplier;

    public float explosionRadius;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Ground")) {
            Debug.Log("hit");
            // VFX HIT GROUND
        }
    }

    public void StartExplosionSequence() {

        // HIT

        Invoke("Explode", 2);
    }

    private void Explode() {

        // EXPLOSIONN FEEDBACK

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders) {
            Block block = collider.GetComponent<Block>();
            if(block != null) {
                block.TakeDamage(1, Vector3.zero, Vector3.zero);
            }
        }

        Destroy(gameObject);
    }
}
