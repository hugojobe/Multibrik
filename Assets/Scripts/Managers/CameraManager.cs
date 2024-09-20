using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;

	public List<GameObject> ships;

	public CinemachineVirtualCamera leftZoomCam;
	public CinemachineVirtualCamera rightZoomCam;

	public CinemachineMixingCamera mixCam;

	public float maxBlend = 0.1f;

	public float startingBlendDistance;

	private void Awake() {
		instance = this;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;

		foreach(GameObject ship in ships) {
			Gizmos.DrawWireSphere(ship.transform.position, startingBlendDistance);

		}
	}

	public void InitializeCameras() {
		leftZoomCam.Follow = ships[0].transform;
		rightZoomCam.Follow = ships[1].transform;
	}

    /*private void Update() {
        if(GameManager.instance.hasGameStarted) {
            foreach(GameObject ship in ships) {
                float distance = Vector3.Distance(ship.transform.position, BallController.instance.transform.position);
                if(distance <= startingBlendDistance) {
                    float weight = Mathf.Lerp(0f, maxBlend, 1f - (distance / startingBlendDistance));
                    if(ship == ships[0]) {
						mixCam.m_Weight1 = weight;
                    } else if(ship == ships[1]) {
						mixCam.m_Weight2 = weight;
                    }
                }
            }
        }
    }*/
}
