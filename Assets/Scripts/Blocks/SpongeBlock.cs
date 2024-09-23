using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeBlock : Block
{
    public int ballSpeedMultiplier;

    public override void OnDestruction() {
        base.OnDestruction();

        Destroy(gameObject);
    }

    public override void OnDamageTaken(Vector3 contactPosition, Vector3 contactNormal)
    {
        base.OnDamageTaken(contactPosition, contactNormal);
        Instantiate(hitVFX, transform.position, hitVFX.transform.rotation);
    }
}
