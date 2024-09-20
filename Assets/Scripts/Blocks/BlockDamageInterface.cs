using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BlockDamageInterface
{
    public void TakeDamage(int damage, Vector3 contactPosition, Vector3 contactNormal);
}
