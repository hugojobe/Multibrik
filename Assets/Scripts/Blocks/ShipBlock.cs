using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlock : Block
{
    public int playerIndex;

    public override void OnDamageTaken() {
        GameManager.instance.GameOver(playerIndex);

        // DAMAGE FEEDBACKS
    }
}
