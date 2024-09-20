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

    public void StartExplosionSequence() {
        // EXPLOSIONN FEEDBACK

        Invoke("Explode", 2);
    }

    private void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders) {
            Block block = collider.GetComponent<Block>();
            if(block != null) {
                block.TakeDamage(1);
            }
        }

        Destroy(gameObject);
    }
}
