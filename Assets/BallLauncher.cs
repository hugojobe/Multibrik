using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour {
    public GameObject ballRotator;
    public BallController ball;

    public float spinRate;
    public float spinRateModifier;

    public GameObject ballLaunchCanvas;

    private List<(float, float)> allowedRanges = new List<(float, float)> {
        (25, 80),
        (100, 155),
        (205, 260),
        (280, 335)
    };

    public void LaunchBall(float delay) {
        StartCoroutine(LaunchCoroutine(delay));
    }

    private IEnumerator LaunchCoroutine(float delay) {
        int randomIndex = Random.Range(0, allowedRanges.Count);
        var selectedRange = allowedRanges[randomIndex];

        float targetAngle = Random.Range(selectedRange.Item1, selectedRange.Item2);

        float finalSpinRate = Random.Range(spinRate - spinRateModifier, spinRate + spinRateModifier);

        float angleToRotate;
        if(finalSpinRate < 0) {
            angleToRotate = 360 + (360 - targetAngle);
        } else {
            angleToRotate = 360 + targetAngle;
        }

        float timeToRotate = (angleToRotate / Mathf.Abs(finalSpinRate));

        float elapsedTime = 0f;
        while(elapsedTime < timeToRotate) {
            ballRotator.transform.Rotate(Vector3.up, finalSpinRate * Time.deltaTime, Space.Self);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        OnRotationFinished(angleToRotate - 360);
    }

    private void OnRotationFinished(float targetAngle) {
        float localRotationX = ballRotator.transform.localEulerAngles.x;

        if(100 < targetAngle && targetAngle < 155)
            localRotationX = 180 - localRotationX;
        else if(205 < targetAngle && targetAngle < 260)
            localRotationX = 180 + (360 - localRotationX);

        Vector3 directionX = new Vector3(Mathf.Cos(localRotationX * Mathf.Deg2Rad), 0, Mathf.Sin(localRotationX * Mathf.Deg2Rad));

        ballLaunchCanvas.SetActive(false);

        ball.ReplaceVelocity(directionX.normalized * ball.ballInitialSpeed);
    }

    private void DrawDebugLines() {
        foreach(var range in allowedRanges) {
            DrawDebugLineForRange(range.Item1, Color.green);
            DrawDebugLineForRange(range.Item2, Color.red);
        }
    }

    private void DrawDebugLineForRange(float angle, Color color) {
        Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, -Mathf.Sin(angle * Mathf.Deg2Rad));
        Debug.DrawLine(ballRotator.transform.position, ballRotator.transform.position + direction * 5, color, 2f);
    }
}
