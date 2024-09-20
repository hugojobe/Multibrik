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
}
