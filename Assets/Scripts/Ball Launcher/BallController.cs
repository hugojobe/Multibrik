using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	public static BallController instance;

	public Rigidbody rb;

	public int baseDamage;
	public int damageMultiplier;

	public float ballInitialSpeed;

	private bool resetSpeedNextBounce;

	private void Awake() {
		instance = this;
	}
	public void ReplaceVelocity(Vector3 direction) {
		direction = new Vector3(direction.x, 0, direction.z);

		rb.velocity = direction;
	}

	private void Update() {
		if(GameManager.instance.hasGameStarted){
			bool isRightSide = transform.position.x > 0;

			if(GameManager.instance.playerBuildingSystems.Count > 0){
				GameManager.instance.playerBuildingSystems[0].canGhostBeBuilt = isRightSide;
				GameManager.instance.playerBuildingSystems[1].canGhostBeBuilt = !isRightSide;
			}
		}
	}
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Bounce")) {
            HandleBounceCollision(other);
        } else if(other.gameObject.CompareTag("Explosive") || other.gameObject.CompareTag("Sponge") || other.gameObject.CompareTag("Rotoballe")) {
            HandleSpecialCollision(other);
        }
    }

    private void HandleBounceCollision(Collider other) {
        if(resetSpeedNextBounce) {
            rb.velocity = rb.velocity.normalized * ballInitialSpeed;
            resetSpeedNextBounce = false;
            damageMultiplier = 1;
        }

        Vector3 normal = GetCollisionNormal(other);
        rb.velocity = Vector3.Reflect(rb.velocity, normal);

        if(other.TryGetComponent(out BlockDamageInterface block)) {
            block.TakeDamage(baseDamage * damageMultiplier, other.ClosestPoint(transform.position), other.ClosestPointOnBounds(transform.position) - transform.position);
        }
    }

    private void HandleSpecialCollision(Collider other) {
        resetSpeedNextBounce = true;

        if(other.TryGetComponent(out ExplosiveBlock explosiveBlock)) {
            explosiveBlock.StartExplosionSequence();
            explosiveBlock.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * ballInitialSpeed * 2, ForceMode.Impulse);
            HandleBounceCollision(other);
        } else if(other.TryGetComponent(out SpongeBlock spongeBlock)) {
            spongeBlock.TakeDamage(1, other.ClosestPoint(transform.position), other.ClosestPointOnBounds(transform.position) - transform.position);
            Vector3 normal = GetCollisionNormal(other);
            rb.velocity = Vector3.Reflect(rb.velocity, normal);
            rb.velocity = rb.velocity.normalized * ballInitialSpeed * spongeBlock.ballSpeedMultiplier;
            damageMultiplier = 2;
        } else if(other.TryGetComponent(out Rotoballe rotoballe)) {
            rotoballe.TakeDamage(1, other.ClosestPoint(transform.position), other.ClosestPointOnBounds(transform.position) - transform.position);
            
            Vector3 directionToEnemyShip = rotoballe.otherShip.transform.position - rotoballe.transform.position;
            directionToEnemyShip = new Vector3(directionToEnemyShip.x, 0, directionToEnemyShip.z);

            rb.velocity = directionToEnemyShip.normalized * ballInitialSpeed;

            if(resetSpeedNextBounce) {
                resetSpeedNextBounce = false;
                damageMultiplier = 1;
            }
        }
    }

    private Vector3 GetCollisionNormal(Collider other) {
        Vector3 normal = other.ClosestPointOnBounds(transform.position) - transform.position;
        return new Vector3(normal.x, 0, normal.z).normalized;
    }

    private void SetRandomVelocity(float speedMultiplier) {
        Vector3 randomNormalizedVector = Random.insideUnitSphere;
        randomNormalizedVector.y = 0;
        randomNormalizedVector.Normalize();

        rb.velocity = randomNormalizedVector * ballInitialSpeed * speedMultiplier;
    }

	
}
