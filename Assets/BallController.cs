using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;

    public Rigidbody rb;

    public int baseDamage;
    public int damageMultiplier;

    private void Awake() {
        instance = this;
    }
    public void ReplaceVelocity(Vector3 direction) {
        direction = new Vector3(direction.x, 0, direction.z);

        rb.velocity = direction;
    }

    private void Update() {
        if(GameManager.instance.hasGameStarted){
            bool isLeftSide = transform.position.x > 0;
            GameManager.instance.playerBuildingSystems[0].canGhostBeBuilt = isLeftSide;
            GameManager.instance.playerBuildingSystems[1].canGhostBeBuilt = !isLeftSide;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Bounce")) {
            Vector3 normal = other.ClosestPointOnBounds(transform.position) - transform.position;
            normal = new Vector3(normal.x, 0, normal.z).normalized;

            Vector3 newVelocity = Vector3.Reflect(rb.velocity, normal);

            Vector3 contactPoint = other.ClosestPoint(transform.position);

            //Debug.DrawLine(contactPoint, contactPoint - rb.velocity, Color.red, 2f);
            //Debug.DrawLine(contactPoint, contactPoint + newVelocity, Color.blue, 2f);

            rb.velocity = newVelocity;

            other.TryGetComponent(out BlockDamageInterface block);
            block?.TakeDamage(baseDamage * damageMultiplier);
        }
    }
}
