using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private TapGestureRecognizer tapGesture = new TapGestureRecognizer();

    private void Start()
    {
        CreateTapGesture();
    }

    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

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
    }

    private void SpeedDrain()
    {
        speed -= 0.001f;

        UIPowerSliderController.instance.UpdatePowerBar(speed);
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
}
