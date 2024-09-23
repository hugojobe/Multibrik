using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonneauTrailOrientation : MonoBehaviour
{
    private Vector3 lastPos;

    void Update()
    {
        Orient();
    }
    private void Orient()
    {
        Vector3 direction = (transform.position - lastPos).normalized;
        transform.LookAt(transform.position + direction);
        lastPos = transform.position;
    }
}
