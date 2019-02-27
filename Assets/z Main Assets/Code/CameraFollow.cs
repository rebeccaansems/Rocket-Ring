using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraOffset;

    private void Update()
    {
        this.transform.position = GameController.instance.Player.transform.position + cameraOffset;
    }
}
