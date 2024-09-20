using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, BlockDamageInterface {
    public int baseHealth;
    public int currentHealth;

    public Tile correspondingTile;

    private void Awake() {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            OnDestruction();
        } else {
            OnDamageTaken();
        }
    }

    public virtual void OnDamageTaken() {}

    public virtual void OnDestruction(){
        correspondingTile.isEmpty = true;
    }
}