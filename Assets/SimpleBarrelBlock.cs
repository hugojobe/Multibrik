using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBarrelBlock : Block
{
    public override void OnDamageTaken() {
        base.OnDamageTaken();

        // DAMAGE FEEDBACKS
    }

    public override void OnDestruction() {
        base.OnDestruction();

        // DESTRUCTION FEEDBACKS

        Destroy(gameObject);
    }
}
