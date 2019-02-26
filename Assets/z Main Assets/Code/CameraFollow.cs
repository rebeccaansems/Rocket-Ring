using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 cameraOffset;

    private void Update()
    {
        this.transform.position = player.transform.position + cameraOffset;
    }
}
