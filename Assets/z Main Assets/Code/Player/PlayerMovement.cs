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
    }

    public void SpeedBoost()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * speed * 2, ForceMode.Impulse);
        speed += 0.1f;
    }
}
