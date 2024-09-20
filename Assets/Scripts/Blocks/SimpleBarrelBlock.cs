using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBarrelBlock : Block
{
    public override void OnDamageTaken(Vector3 contactPosition, Vector3 contactNormal) {
        base.OnDamageTaken(contactPosition, contactNormal);

        // DAMAGE FEEDBACKS
    }

    public override void OnDestruction() {
        base.OnDestruction();

        // DESTRUCTION FEEDBACKS

        Destroy(gameObject);
    }
}
