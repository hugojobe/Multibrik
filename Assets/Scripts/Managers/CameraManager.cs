using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject leftShip;
    public GameObject rightShip;

    public CinemachineVirtualCamera leftZoomCam;
    public CinemachineVirtualCamera rightZoomCam;

    public CinemachineBlendListCamera blendCam;

    public float maxBlend = 0.3f;

    private void Update() {
        
    }
}
