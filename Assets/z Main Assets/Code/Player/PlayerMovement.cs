using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve[] xCurve, rotationCurve;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float[] flyingLaneX;

    private int currentLane = 2, laneDirection;
    private float timeElapsed, startPosX, startPosY;
    private bool laneJumping = false;
    private Vector3 eulerAngleVelocity;


    private TapGestureRecognizer tapGesture = new TapGestureRecognizer();

    private SwipeGestureRecognizer swipeLeftGesture = new SwipeGestureRecognizer();
    private SwipeGestureRecognizer swipeRightGesture = new SwipeGestureRecognizer();

    void Start()
    {
        eulerAngleVelocity = new Vector3(0, 0, 0);

        CreateTapGesture();
        CreateSwipeGesture();
    }

    private void FixedUpdate()
    {
        float xMove = 0;
        float yMove = 0;

        if (laneJumping == false)
        {
            timeElapsed = 0;
            startPosX = transform.position.x;

            xMove = startPosX;

            this.GetComponent<Rigidbody>().MovePosition(new Vector2(
                startPosX,
                startPosY + (speed * Time.deltaTime)
                ));
        }
        else
        {
            timeElapsed += Time.deltaTime;

            xMove = startPosX + xCurve[laneDirection].Evaluate(timeElapsed);

            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, rotationCurve[laneDirection].Evaluate(timeElapsed)));
            this.GetComponent<Rigidbody>().MoveRotation(deltaRotation);

            if (timeElapsed > xCurve[laneDirection].keys[xCurve[laneDirection].length - 1].time)
            {
                laneJumping = false;
            }
        }

        startPosY = transform.position.y;
        yMove = startPosY + (speed * Time.deltaTime);

        this.GetComponent<Rigidbody>().MovePosition(new Vector2(xMove, yMove));

        SpeedDrain();
    }

    public void SpeedBoost(bool isManual)
    {
        if (isManual)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 6, ForceMode.Impulse);
            speed -= 0.25f;
        }
        else
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * speed * 2, ForceMode.Impulse);
            speed += 0.15f;
        }

        UIPowerSliderController.instance.UpdatePowerBar(speed);
        StartCoroutine(ZoomAnimator());
    }

    private void SpeedDrain()
    {
        speed -= 0.001f;

        UIPowerSliderController.instance.UpdatePowerBar(speed);
    }

    private void LaneJump(int direction)
    {
        if (laneJumping == false)
        {
            int prevLane = currentLane;
            currentLane += direction;
            currentLane = Mathf.Clamp(currentLane, 0, flyingLaneX.Length - 1);

            if (prevLane != currentLane)
            {
                laneDirection = Mathf.Clamp(direction, 0, 1);
                laneJumping = true;
            }
        }
    }

    IEnumerator ZoomAnimator()
    {
        this.GetComponentInChildren<Animator>().SetBool("isZooming", true);

        yield return new WaitForSeconds(0.5f);
        this.GetComponentInChildren<Animator>().SetBool("isZooming", false);
    }

    #region Gestures
    private void CreateSwipeGesture()
    {
        swipeLeftGesture = new SwipeGestureRecognizer();
        swipeLeftGesture.Direction = SwipeGestureRecognizerDirection.Left;
        swipeLeftGesture.StateUpdated += SwipeLeftGestureCallback;
        FingersScript.Instance.AddGesture(swipeLeftGesture);

        swipeRightGesture = new SwipeGestureRecognizer();
        swipeRightGesture.Direction = SwipeGestureRecognizerDirection.Right;
        swipeRightGesture.StateUpdated += SwipeRightGestureCallback;
        FingersScript.Instance.AddGesture(swipeRightGesture);
    }

    private void SwipeLeftGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            LaneJump(-1);
        }
    }

    private void SwipeRightGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            LaneJump(1);
        }
    }

    private void CreateTapGesture()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.NumberOfTapsRequired = 1;
        tapGesture.StateUpdated += TapGestureCallback;
        FingersScript.Instance.AddGesture(tapGesture);
    }

    private void TapGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            SpeedBoost(true);
        }
    }
    #endregion
}
