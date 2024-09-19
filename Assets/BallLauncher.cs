using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballRotator;
    public BallController ball;

    public float spinRate;
    public float spinRateModifier;

    public GameObject ballLaunchCanvas;

    public float ballInitialSpeed;

    public void LaunchBall(float delay) {
        StartCoroutine(LaunchCoroutine(delay));
    }

    private IEnumerator LaunchCoroutine(float delay) {
        float elapsedTime = 0f;
        float finalSpinRate = Random.Range(spinRate - spinRateModifier, spinRate + spinRateModifier);
        finalSpinRate = finalSpinRate * (Random.value > 0.5f ? 1 : -1);


        while(elapsedTime < delay) {
            ballRotator.transform.Rotate(Vector3.up, finalSpinRate * Time.deltaTime, Space.Self);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        OnRotationFinished();
    }

    private void OnRotationFinished() {
        float localRotationX = ballRotator.transform.localEulerAngles.x;
        localRotationX = localRotationX % 360;

        Vector3 directionX = new Vector3(Mathf.Cos(localRotationX * Mathf.Deg2Rad), 0, -Mathf.Sin(localRotationX * Mathf.Deg2Rad));
        //Debug.DrawLine(ballRotator.transform.position, ballRotator.transform.position + directionX * 5, Color.blue, 2f);

        ballLaunchCanvas.SetActive(false);

        ball.ReplaceVelocity(directionX.normalized * ballInitialSpeed);
    }
}
