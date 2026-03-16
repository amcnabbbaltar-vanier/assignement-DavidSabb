using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private float startCameraZ;

    void Start()
    {
        startCameraZ = transform.position.z;
    }

    void LateUpdate()
    {
        float distanceFromZero = Mathf.Abs(player.position.y);

        Vector3 pos = transform.position;
        pos.z = startCameraZ - distanceFromZero;

        transform.position = pos;
    }
}