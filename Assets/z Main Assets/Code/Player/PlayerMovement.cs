using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        SpeedDrain();
    }

    public void SpeedBoost()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * speed * 2, ForceMode.Impulse);
        speed += 0.15f;

        UIPowerSliderController.instance.UpdatePowerBar(speed);
    }

    private void SpeedDrain()
    {
        speed -= 0.001f;

        UIPowerSliderController.instance.UpdatePowerBar(speed);
    }
}
