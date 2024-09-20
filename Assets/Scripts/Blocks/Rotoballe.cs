using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotoballe : Block
{
    public ShipBlock otherShip;

    public override void OnDestruction() {
        base.OnDestruction();

        Destroy(gameObject);
    }
}
