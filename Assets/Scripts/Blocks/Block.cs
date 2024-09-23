using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, BlockDamageInterface {
    public int baseHealth;
    public int currentHealth;

    public AnimatedVFX hitVFX;

    public Tile correspondingTile;

    private void Awake() {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damage, Vector3 contactPosition, Vector3 contactNormal) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            OnDestruction();
        } else {
            OnDamageTaken(contactPosition, contactNormal);
        }
    }

    public virtual void OnDamageTaken(Vector3 contactPosition, Vector3 contactNormal) {}

    public virtual void OnDestruction(){
        correspondingTile.isEmpty = true;
    }
}