using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, BlockDamageInterface {
    public int baseHealth;
    public int currentHealth;

    

    public Tile correspondingTile;

    //VFX Related
    [SerializeField]
    private float dmgBlinkFreq = 2f;
    [SerializeField]
    private Renderer rend;
    public AnimatedVFX hitVFX;
    private MaterialPropertyBlock mpb;
    private float customTime = 0f;


    private void Awake() {
        currentHealth = baseHealth;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        mpb = new MaterialPropertyBlock();
    }

    public void TakeDamage(int damage, Vector3 contactPosition, Vector3 contactNormal) {
        OnDamageTaken(contactPosition, contactNormal);
        currentHealth -= damage;
        SetDamageIndicator();
        if(currentHealth <= 0) {
            OnDestruction();
        }
    }

    public virtual void OnDamageTaken(Vector3 contactPosition, Vector3 contactNormal) {}

    public virtual void OnDestruction(){
        correspondingTile.isEmpty = true;
    }

    private void SetDamageIndicator(){
        mpb.SetFloat("_NormalizedHealth", (float)currentHealth/baseHealth);
        rend.SetPropertyBlock(mpb);
    }

    private void Update(){
        if(currentHealth != 1){
            return;
        }
        else{
            customTime += Time.deltaTime * dmgBlinkFreq;
            mpb.SetFloat("_CustomTime", customTime);
            rend.SetPropertyBlock(mpb);
        }
    }
}