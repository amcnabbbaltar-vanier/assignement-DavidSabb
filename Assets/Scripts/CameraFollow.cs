using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private float startPlayerHeight;
    private float startCameraZ;

    void Start()
    {
        startPlayerHeight = player.position.y;
        startCameraZ = transform.position.z;
    }

    void LateUpdate()
    {
        float heightDifference = player.position.y - startPlayerHeight;

        Vector3 pos = transform.position;
        pos.z = startCameraZ - heightDifference;

        transform.position = pos;
    }
}
