using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlock : Block
{
    public int playerIndex;

    public override void OnDamageTaken(Vector3 contactPosition, Vector3 contactNormal) {
        GameManager.instance.GameOver(playerIndex);

        // DAMAGE FEEDBACKS
    }
}
