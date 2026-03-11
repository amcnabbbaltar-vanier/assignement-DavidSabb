using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHover : MonoBehaviour
{
    public float speed = 1f;

    public float maxHeight = 0.5f;

    private float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;   
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * maxHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
